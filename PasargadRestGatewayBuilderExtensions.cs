// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Parbad.GatewayBuilders;
using PasargadRest.Parbad.Gateway.Api;
using PasargadRest.Parbad.Gateway.Internal;
using System;
using System.Net.Http;

namespace PasargadRest.Parbad.Gateway;

public static class PasargadRestGatewayBuilderExtensions
{
	/// <summary>
	/// Adds Pasargad gateway to Parbad services.
	/// </summary>
	/// <param name="builder"></param>
	public static IGatewayConfigurationBuilder<PasargadRestGateway> AddPasargadRest(this IGatewayBuilder builder)
	{
		if (builder == null) throw new ArgumentNullException(nameof(builder));

		return builder
			  .AddGateway<PasargadRestGateway>()
			  .WithHttpClient<PasargadRestApi>((serviceProvider, httpClient) =>
										   {
											   var gatewayOptions = serviceProvider.GetRequiredService<IOptions<PasargadRestGatewayOptions>>();

											   httpClient.BaseAddress = new Uri(gatewayOptions.Value.ApiBaseUrl);
										   })
			  .WithOptions(options => { });
	}

	/// <summary>
	/// Configures the HttpClient for <see cref="IPasargadRestApi"/>.
	/// </summary>
	/// <typeparam name="TGatewayApi">Implementation type of <see cref="IPasargadRestApi"/>.</typeparam>
	/// <exception cref="ArgumentNullException"></exception>
	public static IGatewayConfigurationBuilder<PasargadRestGateway> WithHttpClient<TGatewayApi>(this IGatewayConfigurationBuilder<PasargadRestGateway> builder,
																							Action<IServiceProvider, HttpClient> configureHttpClient,
																							Action<IHttpClientBuilder> configureHttpClientBuilder = null)
		where TGatewayApi : class, IPasargadRestApi
	{
		if (builder == null) throw new ArgumentNullException(nameof(builder));
		if (configureHttpClient == null) throw new ArgumentNullException(nameof(configureHttpClient));

		builder.WithHttpClient<IPasargadRestApi, TGatewayApi>(configureHttpClient, configureHttpClientBuilder);

		return builder;
	}

	/// <summary>
	/// Configures the accounts for <see cref="PasargadRestGateway"/>.
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="configureAccounts">Configures the accounts.</param>
	public static IGatewayConfigurationBuilder<PasargadRestGateway> WithAccounts(
		this IGatewayConfigurationBuilder<PasargadRestGateway> builder,
		Action<IGatewayAccountBuilder<PasargadRestGatewayAccount>> configureAccounts)
	{
		if (builder == null) throw new ArgumentNullException(nameof(builder));

		return builder.WithAccounts(configureAccounts);
	}

	/// <summary>
	/// Configures the options for Pasargad Gateway.
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="configureOptions">Configuration</param>
	public static IGatewayConfigurationBuilder<PasargadRestGateway> WithOptions(
		this IGatewayConfigurationBuilder<PasargadRestGateway> builder,
		Action<PasargadRestGatewayOptions> configureOptions)
	{
		builder.Services.Configure(configureOptions);

		return builder;
	}

}
