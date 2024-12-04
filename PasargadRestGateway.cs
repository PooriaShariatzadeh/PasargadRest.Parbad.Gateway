// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Parbad;
using Parbad.Abstraction;
using Parbad.GatewayBuilders;
using Parbad.Internal;
using Parbad.Options;
using Parbad.Storage.Abstractions.Models;
using PasargadRest.Parbad.Gateway.Api;
using PasargadRest.Parbad.Gateway.Api.Models;
using PasargadRest.Parbad.Gateway.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PasargadRest.Parbad.Gateway;

[Gateway(Name)]
public class PasargadRestGateway : GatewayBase<PasargadRestGatewayAccount>
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IPasargadRestApi _pasargadApi;
	private readonly PasargadRestGatewayOptions _gatewayOptions;
	private readonly MessagesOptions _messageOptions;

	private const string InvoiceDateKey = "invoiceDate";
	public const string Name = "Pasargad";

	public PasargadRestGateway(IHttpContextAccessor httpContextAccessor,
						   IPasargadRestApi pasargadApi,
						   IGatewayAccountProvider<PasargadRestGatewayAccount> accountProvider,
						   IOptions<PasargadRestGatewayOptions> gatewayOptions,
						   IOptions<MessagesOptions> messageOptions)
		: base(accountProvider)
	{
		_httpContextAccessor = httpContextAccessor;
		_pasargadApi = pasargadApi;
		_gatewayOptions = gatewayOptions.Value;
		_messageOptions = messageOptions.Value;
	}

	/// <inheritdoc />
	public override async Task<IPaymentRequestResult> RequestAsync(Invoice invoice, CancellationToken cancellationToken = default)
	{
		if (invoice == null) throw new ArgumentNullException(nameof(invoice));

		var account = await GetAccountAsync(invoice).ConfigureAwaitFalse();

		var invoiceDate = PasargadRestHelper.GetTimeStamp();
		var timeStamp = invoiceDate;

		var additionalData = invoice.GetPasargadRequestAdditionalData();

		var response = await _pasargadApi.Purchase(new PurchaseRequest
		{
			TerminalNumber = account.TerminalNumber,
			Invoice = invoice.TrackingNumber.ToString(),
			InvoiceDate = DateTime.Now.Date.ToString(),
			Amount = (int)invoice.Amount,
			CallbackApi = invoice.CallbackUrl,
			Description = string.Empty,
			PayerMail = string.Empty,
			Pans = string.Empty,
			PayerName = string.Empty,
			MobileNumber = string.Empty,
			NationalCode = string.Empty,
			ServiceCode = "8",
			ServiceType = "PURCHASE",
		},
												   account.Username, account.Password,
												   cancellationToken)
										 .ConfigureAwaitFalse();

		if (response.ResultCode != 0)
		{
			return PaymentRequestResult.Failed(response.ResultMsg, account.Name);
		}

		var form = new Dictionary<string, string>
				   {
					   { "Token", response.Data.UrlId }
				   };

		var result = PaymentRequestResult.SucceedWithPost(account.Name,
														  _httpContextAccessor.HttpContext,
														  response.Data.Url,
														  form);

		result.DatabaseAdditionalData.Add(InvoiceDateKey, invoiceDate);
		result.DatabaseAdditionalData.Add("UrlId", response.Data.UrlId);

		return result;
	}

	/// <inheritdoc />
	public override async Task<IPaymentFetchResult> FetchAsync(InvoiceContext context, CancellationToken cancellationToken = default)
	{
		if (context == null) throw new ArgumentNullException(nameof(context));

		var callbackResult = await PasargadRestHelper.BindCallbackResultModel(_httpContextAccessor.HttpContext.Request,
																		  cancellationToken)
												 .ConfigureAwaitFalse();

		if (!callbackResult.IsSucceed)
		{
			return PaymentFetchResult.Failed(_messageOptions.PaymentFailed);
		}

		var result = PaymentFetchResult.ReadyForVerifying();
		result.TransactionCode = callbackResult.TransactionReferenceId;

		return result;
	}

	/// <inheritdoc />
	public override async Task<IPaymentVerifyResult> VerifyAsync(InvoiceContext context, CancellationToken cancellationToken = default)
	{
		if (context == null) throw new ArgumentNullException(nameof(context));

		var callbackResult = await PasargadRestHelper.BindCallbackResultModel(_httpContextAccessor.HttpContext.Request, cancellationToken).ConfigureAwaitFalse();

		if (!callbackResult.IsSucceed)
		{
			return PaymentVerifyResult.Failed(_messageOptions.PaymentFailed);
		}

		var account = await GetAccountAsync(context.Payment).ConfigureAwaitFalse();

		var requestTransaction = context.Transactions.SingleOrDefault(transaction => transaction.Type == TransactionType.Request);
		var requestTransactionAdditionalData = requestTransaction.ToDictionary();

		if (!requestTransactionAdditionalData.TryGetValue("UrlId", out var UrlId))
		{
			return (IPaymentVerifyResult)PaymentRefundResult.Failed($"UrlId for Invoice {context.Payment.TrackingNumber} not found");
		}

		var response = await _pasargadApi.VerifyPayment(new ConfirmPaymentRequestModel
		{
			Invoice = context.Payment.TrackingNumber.ToString(),
			UrlId = UrlId,
		}, account.Username, account.Password, cancellationToken).ConfigureAwaitFalse();

		if (!response.IsSuccess)
		{
			return PaymentVerifyResult.Failed(response.ResultMsg ?? _messageOptions.PaymentFailed);
		}

		return PaymentVerifyResult.Succeed(callbackResult.TransactionReferenceId,
										   _messageOptions.PaymentSucceed);
	}

	/// <inheritdoc />
	public override async Task<IPaymentRefundResult> RefundAsync(InvoiceContext context, Money amount, CancellationToken cancellationToken = default)
	{
		if (context == null) throw new ArgumentNullException(nameof(context));

		var account = await GetAccountAsync(context.Payment).ConfigureAwaitFalse();

		var requestTransaction = context.Transactions.SingleOrDefault(transaction => transaction.Type == TransactionType.Request);

		if (requestTransaction == null)
		{
			return PaymentRefundResult.Failed($"Transaction for Invoice {context.Payment.TrackingNumber} not found");
		}

		var requestTransactionAdditionalData = requestTransaction.ToDictionary();

		if (!requestTransactionAdditionalData.TryGetValue(InvoiceDateKey, out var invoiceDate))
		{
			return PaymentRefundResult.Failed($"InvoiceDate for Invoice {context.Payment.TrackingNumber} not found");
		}
		if (!requestTransactionAdditionalData.TryGetValue("UrlId", out var UrlId))
		{
			return PaymentRefundResult.Failed($"UrlId for Invoice {context.Payment.TrackingNumber} not found");
		}
		var response = await _pasargadApi.RefundPayment(new ReversePaymentRequestModel
		{

			Invoice = context.Payment.TrackingNumber.ToString(),
			UrlId = UrlId,
		},account.Username, account.Password,cancellationToken).ConfigureAwaitFalse();

		if (!response.IsSuccess)
		{
			return PaymentRefundResult.Failed(response.ResultMsg ?? "Refund failed.");
		}

		return PaymentRefundResult.Succeed();
	}
}
