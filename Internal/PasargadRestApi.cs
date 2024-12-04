// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Parbad.Gateway.Pasargad;
using Parbad.Net;
using PasargadRest.Parbad.Gateway.Api;
using PasargadRest.Parbad.Gateway.Api.Models;

namespace PasargadRest.Parbad.Gateway.Internal;

internal class PasargadRestApi : IPasargadRestApi
{
	private readonly HttpClient _httpClient;
	private readonly PasargadRestGatewayOptions _options;

	public PasargadRestApi(HttpClient httpClient, IOptions<PasargadRestGatewayOptions> options)
	{
		_httpClient = httpClient;
		_options = options.Value;
	}

	internal async Task<GetTokenResponseModel> GetToken(string username, string password, CancellationToken cancellationToken)
	{
		return await _httpClient.PostJsonAsync<GetTokenResponseModel>(_options.TokenUrl, new { username, password });
	}

	public async Task<PurchaseResponse> Purchase(PurchaseRequest model,
														string username, string password,
														CancellationToken cancellationToken)
	{

		var token = await GetToken(username, password, cancellationToken);
		_httpClient.DefaultRequestHeaders.AddOrUpdate("Authorization", $"Bearer {token.token}");
		var result = await _httpClient.PostJsonAsync<PurchaseResponse>(_options.PurchaseUrl, model, cancellationToken: cancellationToken);
		return result;
	}


	public async Task<ConfirmPaymentResponseModel> VerifyPayment(ConfirmPaymentRequestModel model,
																string username, string password,
																  CancellationToken cancellationToken)
	{
		var token = await GetToken(username, password, cancellationToken);
		_httpClient.DefaultRequestHeaders.AddOrUpdate("Authorization", $"Bearer {token.token}");
		var result = await _httpClient.PostJsonAsync<ConfirmPaymentResponseModel>(_options.ConfirmUrl, model, cancellationToken: cancellationToken);
		return result;
	}

	public async Task<ReversePaymentResponseModel> RefundPayment(ReversePaymentRequestModel model,
																string username, string password,
																  CancellationToken cancellationToken)
	{
		var token = await GetToken(username, password, cancellationToken);
		_httpClient.DefaultRequestHeaders.AddOrUpdate("Authorization", $"Bearer {token.token}");
		var result = await _httpClient.PostJsonAsync<ReversePaymentResponseModel>(_options.ReverseUrl, model, cancellationToken: cancellationToken);
		return result;
	}

}
