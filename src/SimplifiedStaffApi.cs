using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    /// <summary>
    /// Simplified version of the API, allowing you to get only the right <see cref="Staff"/> instead of dealing with the <see cref="FreshbooksResponse"/> object.
    /// </summary>
    /// <remarks>
    /// This class with throw an <see cref="InvalidOperationException"/> when the freshbooks response status is different than "ok"
    /// </remarks>
    public class SimplifiedStaffApi : StaffApi
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedStaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public SimplifiedStaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks) 
            : base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedStaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public SimplifiedStaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the information about the currently authentified user in Freshbooks, that is the account which the <c>Freshbooks</c> client uses.
        /// </summary>
        public Staff Current 
        { 
            get
            {
                var currentResponse = base.CallGetCurrent();

                if (currentResponse.Status)
                    return currentResponse.Result;

                throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Status));
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get the staff member associated with the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">The <c>staff_id</c> identifying this person in Freshbooks</param>
        /// <returns>The complete <see cref="Staff"/> information resumed in a single class instance</returns>
        public Staff Get(string id)
        {
            var currentResponse = CallGet(id);

            if (currentResponse.Status)
                return currentResponse.Result;

            throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Status));
        }

        /// <summary>
        /// Get a list of staff member from Freshbooks using the pagination parameters passed in.
        /// </summary>
        /// <param name="page">The page that will be fetch from Freshbooks</param>
        /// <param name="itemPerPage">The number of items to retrieve per page</param>
        /// <returns>
        /// A <see cref="FreshbooksPagedResponse&lt;Staff&gt;"/> giving the current page result as well as indications
        /// on the total number of pages and the total number of items available on the Freshbook server
        /// </returns>
        public FreshbooksPagedResponse<Staff> GetList(int page = 1, int itemPerPage = 100)
        {
            var currentResponse = CallGetList(page, itemPerPage);

            if (currentResponse.Status)
                return currentResponse;

            throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Status));
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <see cref="GetList" /> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<Staff> GetAll()
        {
            var response = CallGetAllPages().ToList();

            if (response.All(r => r.Status))
                return response.SelectMany(m => m.Result);

            var failedResponseStatuses = response.Where(w => !w.Status).Select(s=>s.Status);
            throw new InvalidOperationException(string.Format("Freshbooks API failed with the following statuses : {0}", string.Join(", ", failedResponseStatuses)));
        }

        #endregion
    }
}