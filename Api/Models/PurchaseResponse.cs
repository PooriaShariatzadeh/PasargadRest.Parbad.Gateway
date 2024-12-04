using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasargadRest.Parbad.Gateway.Api.Models
{
	/// <summary>
	/// Response model for the purchase transaction.
	/// </summary>
	public class PurchaseResponse
	{
		/// <summary>
		/// Status message of the request.
		/// </summary>
		public string ResultMsg { get; set; }

		/// <summary>
		/// Code indicating the result of the request.
		/// </summary>
		public int ResultCode { get; set; }

		/// <summary>
		/// Additional data related to the transaction.
		/// </summary>
		public PurchaseResponseData Data { get; set; }
	}

	/// <summary>
	/// Additional data model for the purchase response.
	/// </summary>
	public class PurchaseResponseData
	{
		/// <summary>
		/// Unique identifier for the generated payment URL.
		/// </summary>
		public string UrlId { get; set; }

		/// <summary>
		/// The payment URL where the customer is redirected.
		/// </summary>
		public string Url { get; set; }
	}
}
