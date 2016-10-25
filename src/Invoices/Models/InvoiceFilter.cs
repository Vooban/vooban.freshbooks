using HastyAPI;
using System;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks.Invoices.Models
{
    public class InvoiceFilter : FreshbooksFilter
    {
        #region Properties

        /// <summary>
        /// Gets the projects's unique identifier to which this time entry is associated
        /// </summary>

        public string ClientId { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }


        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new FriendlyDynamic();

            if (!string.IsNullOrEmpty(ClientId)) result.client_id = ClientId;

            if (From != null) result.date_from = From;

            if (To != null) result.date_to = To;

            return result;
        }

        #endregion
    }
}