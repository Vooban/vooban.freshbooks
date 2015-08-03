using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastyAPI;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks.Staff.Models
{
    public class StaffFilter : FreshbooksFilter
    {
        #region Properties

        /// <summary>
        /// Gets the projects's unique identifier to which this time entry is associated
        /// </summary>
        public string FreshbookId { get; set; }

        /// <summary>
        /// Gets the task's unique identifier to which this time entry is associated
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new FriendlyDynamic();

            if (!string.IsNullOrEmpty(FreshbookId)) result.staff_id = FreshbookId;
            if (!string.IsNullOrEmpty(Email)) result.email = Email;

            return result;
        }

        #endregion
    }
}
