// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace PasargadRest.Parbad.Gateway.Api.Models;



public class GetTokenResponseModel
{
	public string resultMsg { get; set; }
	public int resultCode { get; set; }
	public string token { get; set; }
	public string username { get; set; }
	public string firstName { get; set; }
	public string lastName { get; set; }
	public string userId { get; set; }
	public List<Role> roles { get; set; }
}

public class Role
{
	public string authority { get; set; }
}