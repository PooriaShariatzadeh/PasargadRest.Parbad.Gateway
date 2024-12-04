﻿// Copyright (c) Parbad. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC License, Version 3.0. See License.txt in the project root for license information.

using System;
using Parbad;
using Parbad.Abstraction;
using Parbad.InvoiceBuilder;

namespace PasargadRest.Parbad.Gateway;

public static class PasargadRestGatewayInvoiceBuilderExtensions
{
	private const string RequestAdditionalDataKey = nameof(RequestAdditionalDataKey);

	/// <summary>
	/// The invoice will be sent to Pasargad gateway.
	/// </summary>
	/// <param name="builder"></param>
	public static IInvoiceBuilder UsePasargad(this IInvoiceBuilder builder)
	{
		if (builder == null) throw new ArgumentNullException(nameof(builder));

		return builder.SetGateway(PasargadRestGateway.Name);
	}

	/// <summary>
	/// Sets additional data that will be sent to Pasargad gateway when requesting a token.
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	public static IInvoiceBuilder SetPasargadData(this IInvoiceBuilder builder, PasargadRestRequestAdditionalData additionalData)
	{
		if (builder == null) throw new ArgumentNullException(nameof(builder));
		if (additionalData == null) throw new ArgumentNullException(nameof(additionalData));

		return builder.AddOrUpdateProperty(RequestAdditionalDataKey, additionalData);
	}

	internal static PasargadRestRequestAdditionalData GetPasargadRequestAdditionalData(this Invoice invoice)
	{
		if (invoice == null) throw new ArgumentNullException(nameof(invoice));

		if (!invoice.Properties.TryGetValue(RequestAdditionalDataKey, out var additionalData))
		{
			return null;
		}

		return (PasargadRestRequestAdditionalData)additionalData;
	}
}
