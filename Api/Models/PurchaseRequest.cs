using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasargadRest.Parbad.Gateway.Api.Models
{
	/// <summary>
	/// Request model for initiating a purchase transaction.
	/// </summary>
	public class PurchaseRequest
	{
		/// <summary>
		/// Unique identifier for the transaction.
		/// </summary>
		public string Invoice { get; set; }

		/// <summary>
		/// Date of the invoice in the required format.
		/// </summary>
		public string InvoiceDate { get; set; }

		/// <summary>
		/// Transaction amount in Rials.
		/// </summary>
		public int Amount { get; set; }

		/// <summary>
		/// URL to redirect the user after payment.
		/// </summary>
		public string CallbackApi { get; set; }

		/// <summary>
		/// Customer's mobile number (optional).
		/// </summary>
		public string MobileNumber { get; set; }

		/// <summary>
		/// Service code for the type of transaction (e.g., PURCHASE).
		/// </summary>
		public string ServiceCode { get; set; }

		/// <summary>
		/// Service type, such as "PURCHASE".
		/// </summary>
		public string ServiceType { get; set; }

		/// <summary>
		/// Payment terminal number.
		/// </summary>
		public int TerminalNumber { get; set; }

		/// <summary>
		/// Optional description for the transaction.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Email of the payer (optional).
		/// </summary>
		public string PayerMail { get; set; }

		/// <summary>
		/// Name of the payer (optional).
		/// </summary>
		public string PayerName { get; set; }

		/// <summary>
		/// National code of the payer (optional).
		/// </summary>
		public string NationalCode { get; set; }

		/// <summary>
		/// List of card numbers (optional).
		/// </summary>
		public string Pans { get; set; }

		/// <summary>
		/// Payment code of the payer (optional).
		/// </summary>
		public string PaymentCode { get; set; }
	}
}
