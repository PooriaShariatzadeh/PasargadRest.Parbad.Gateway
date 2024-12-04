// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.


// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using Parbad.Abstraction;

namespace PasargadRest.Parbad.Gateway;

public class PasargadRestGatewayAccount : GatewayAccount
{
	public string MerchantCode { get; set; }

	public int TerminalNumber { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
}
