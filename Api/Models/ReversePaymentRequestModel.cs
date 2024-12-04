// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

namespace PasargadRest.Parbad.Gateway.Api.Models;

/// <summary>
/// Request model for reversing a payment transaction via the Pasargad API.
/// </summary>
public class ReversePaymentRequestModel
{
	/// <summary>
	/// The invoice number associated with the transaction to be reversed.
	/// </summary>
	public string Invoice { get; set; }

	/// <summary>
	/// The token (URL ID) received in the response to the purchase request.
	/// </summary>
	public string UrlId { get; set; }
}
