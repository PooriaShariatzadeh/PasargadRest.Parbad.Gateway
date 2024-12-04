// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using System;

namespace PasargadRest.Parbad.Gateway.Api.Models;

/// <summary>
/// Response model for confirming a payment transaction via the Pasargad API.
/// </summary>
public class ConfirmPaymentResponseModel
{
	/// <summary>
	/// Indicates whether the confirmation was successful.
	/// </summary>
	public bool IsSuccess => ResultCode == 0;

	/// <summary>
	/// Message providing details about the result of the confirmation.
	/// </summary>
	public string ResultMsg { get; set; }

	/// <summary>
	/// Code indicating the result of the confirmation request.
	/// </summary>
	public int ResultCode { get; set; }

	/// <summary>
	/// The invoice number provided for the transaction.
	/// </summary>
	public string Invoice { get; set; }

	/// <summary>
	/// Reference number from Shaparak for the transaction.
	/// </summary>
	public string ReferenceNumber { get; set; }

	/// <summary>
	/// Tracking ID for the transaction.
	/// </summary>
	public string TrackId { get; set; }

	/// <summary>
	/// Masked version of the card number used in the transaction.
	/// </summary>
	public string MaskedCardNumber { get; set; }

	/// <summary>
	/// Hashed version of the card number used in the transaction.
	/// </summary>
	public string HashedCardNumber { get; set; }

	/// <summary>
	/// The date and time when the request was made.
	/// </summary>
	public DateTime RequestDate { get; set; }

	/// <summary>
	/// The amount for the transaction.
	/// </summary>
	public int Amount { get; set; }
}

