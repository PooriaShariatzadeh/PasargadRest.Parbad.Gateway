// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Parbad.Http;
using Parbad.Internal;
using PasargadRest.Parbad.Gateway.Internal.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PasargadRest.Parbad.Gateway.Internal;

internal static class PasargadRestHelper
{
	public static async Task<PasargadRestCallbackResultModel> BindCallbackResultModel(HttpRequest httpRequest,
																				  CancellationToken cancellationToken)
	{
		var invoiceNumber = await httpRequest.TryGetParamAsync("invoiceId", cancellationToken).ConfigureAwaitFalse();

		var Status = await httpRequest.TryGetParamAsync("status", cancellationToken).ConfigureAwaitFalse();

		var transactionReferenceId = await httpRequest.TryGetParamAsync("referenceNumber", cancellationToken).ConfigureAwaitFalse();

		return new PasargadRestCallbackResultModel
		{
			InvoiceNumber = invoiceNumber.Value,
			Status = Status.Value,
			TransactionReferenceId = transactionReferenceId.Value
		};
	}

	public static string GetTimeStamp()
	{
		return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
	}
}

