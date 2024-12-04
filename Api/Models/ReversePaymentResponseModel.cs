// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

namespace PasargadRest.Parbad.Gateway.Api.Models;

/// <summary>
/// Response model for reversing a payment transaction via the Pasargad API.
/// </summary>
public class ReversePaymentResponseModel
{
	/// <summary>
	/// Indicates whether the reversal was successful.
	/// </summary>
	public bool IsSuccess => ResultCode == 0;

	/// <summary>
	/// Message providing details about the result of the reversal request.
	/// </summary>
	public string ResultMsg { get; set; }

	/// <summary>
	/// Code indicating the result of the reversal request.
	/// </summary>
	public int ResultCode { get; set; }
}