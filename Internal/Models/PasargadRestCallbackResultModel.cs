// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

namespace PasargadRest.Parbad.Gateway.Internal.Models;

internal class PasargadRestCallbackResultModel
{
	public bool IsSucceed => !string.IsNullOrWhiteSpace(InvoiceNumber) &&
							 !string.IsNullOrWhiteSpace(Status) &&
							 !string.IsNullOrWhiteSpace(TransactionReferenceId);

	public string InvoiceNumber { get; set; }

	public string Status { get; set; }

	public string TransactionReferenceId { get; set; }
}
