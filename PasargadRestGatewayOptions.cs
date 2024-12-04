// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

namespace PasargadRest.Parbad.Gateway;

public class PasargadRestGatewayOptions
{
	public string ApiBaseUrl { get; set; } = "https://pep.shaparak.ir/dorsa1/";

	public string TokenUrl { get; set; } = "token/getToken";

	public string PurchaseUrl { get; set; } = "api/payment/purchase";

	public string ConfirmUrl { get; set; } = "api/payment/confirm-transactions";

	public string ReverseUrl { get; set; } = "api/payment/reverse-transactions";
}
