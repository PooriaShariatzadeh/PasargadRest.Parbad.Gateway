// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;
using PasargadRest.Parbad.Gateway.Api.Models;

namespace PasargadRest.Parbad.Gateway.Api;

/// <summary>
/// API provided by Pasargad Bank.
/// </summary>
public interface IPasargadRestApi
{
	/// <summary>
	/// Gets a token to start a payment request.
	/// </summary>
	Task<PurchaseResponse> Purchase(PurchaseRequest model,
												 string username, string password,
												 CancellationToken cancellationToken);

	/// <summary>
	/// Verifies a payment.
	/// </summary>
	Task<ConfirmPaymentResponseModel> VerifyPayment(ConfirmPaymentRequestModel model,
															string username, string password,
														   CancellationToken cancellationToken);

	/// <summary>
	/// Refunds an already paid invoice.
	/// </summary>
	Task<ReversePaymentResponseModel> RefundPayment(ReversePaymentRequestModel model,
															string username, string password,
														   CancellationToken cancellationToken);
}
