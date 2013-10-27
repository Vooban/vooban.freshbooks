using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;
using Vooban.FreshBooks.DotNet.Api.TimeEntry.Models;

namespace Vooban.FreshBooks.DotNet.Api.TimeEntry
{
    /// <summary>
    /// Simplified version of the API, allowing you to get only the right <see cref="TimeEntryModel"/> instead of dealing with the <see cref="FreshbooksResponse"/> object.
    /// </summary>
    /// <remarks>
    /// This class with throw an <see cref="InvalidOperationException"/> when the freshbooks response status is different than "ok"
    /// </remarks>
    public class SimplifiedTimeEntryApi : TimeEntryApi
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedTimeEntryApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public SimplifiedTimeEntryApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks) 
            : base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplifiedTimeEntryApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public SimplifiedTimeEntryApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

 #region Public methods

        /// <summary>
        /// Get the staff member associated with the provided <paramref name="id"/>
        /// </summary>
        /// <param name="id">The <c>staff_id</c> identifying this person in Freshbooks</param>
        /// <returns>The complete <see cref="StaffModel"/> information resumed in a single class instance</returns>
        public TimeEntryModel Get(string id)
        {
            var currentResponse = CallGet(id);

            if (currentResponse.Success)
                return currentResponse.Result;

            throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Success));
        }

        /// <summary>
        /// Get a list of staff member from Freshbooks using the pagination parameters passed in.
        /// </summary>
        /// <param name="page">The page that will be fetch from Freshbooks</param>
        /// <param name="itemPerPage">The number of items to retrieve per page</param>
        /// <returns>
        /// A <see cref="FreshbooksPagedResponse&lt;StaffModel&gt;"/> giving the current page result as well as indications
        /// on the total number of pages and the total number of items available on the Freshbook server
        /// </returns>
        public FreshbooksPagedResponse<TimeEntryModel> GetList(int page = 1, int itemPerPage = 100)
        {
            var currentResponse = CallGetList(page, itemPerPage);

            if (currentResponse.Success)
                return currentResponse;

            throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Success));
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <see cref="GetList" /> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<TimeEntryModel> GetAll()
        {
            var response = CallGetAllPages().ToList();

            if (response.All(r => r.Success))
                return response.SelectMany(m => m.Result);

            var failedResponseStatuses = response.Where(w => !w.Success).Select(s=>s.Success);
            throw new InvalidOperationException(string.Format("Freshbooks API failed with the following statuses : {0}", string.Join(", ", failedResponseStatuses)));
        }

        /// <summary>
        /// Get a list of staff member from Freshbooks using the pagination parameters passed in.
        /// </summary>
        /// <param name="template">The template used to filter items on the server.</param>
        /// <param name="page">The page that will be fetch from Freshbooks</param>
        /// <param name="itemPerPage">The number of items to retrieve per page</param>
        /// <returns>
        /// A <see cref="FreshbooksPagedResponse&lt;StaffModel&gt;" /> giving the current page result as well as indications
        /// on the total number of pages and the total number of items available on the Freshbook server
        /// </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public FreshbooksPagedResponse<TimeEntryModel> Search(TimeEntryFilter template, int page = 1, int itemPerPage = 100)
        {
            var currentResponse = CallSearch(template, page, itemPerPage);

            if (currentResponse.Success)
                return currentResponse;

            throw new InvalidOperationException(string.Format("Freshbooks API failed with status : {0}", currentResponse.Success));
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <param name="template">The template object used to filter results on the server.</param>
        /// <returns>
        /// The entire content available on Freshbooks
        /// </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        /// <remarks>
        /// This method call the <see cref="GetList" /> method for each available pages and gather all that information into a single list
        /// </remarks>
        public IEnumerable<TimeEntryModel> SearchAll(TimeEntryFilter template)
        {
            var response = CallSearchAll(template).ToList();

            if (response.All(r => r.Success))
                return response.SelectMany(m => m.Result);

            var failedResponseStatuses = response.Where(w => !w.Success).Select(s => s.Success);
            throw new InvalidOperationException(string.Format("Freshbooks API failed with the following statuses : {0}", string.Join(", ", failedResponseStatuses)));
        }

        #endregion
    }
}