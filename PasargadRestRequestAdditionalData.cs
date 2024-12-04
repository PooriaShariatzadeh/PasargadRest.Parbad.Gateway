﻿// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

namespace PasargadRest.Parbad.Gateway;

/// <summary>
/// Additional Data that can be sent to Pasargad gateway when requesting a token.
/// </summary>
public class PasargadRestRequestAdditionalData
{
	public string Mobile { get; set; }

	public string Email { get; set; }

	public string MerchantName { get; set; }

	public string Pidn { get; set; }
}
