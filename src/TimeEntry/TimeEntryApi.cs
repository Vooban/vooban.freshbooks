using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;
using Vooban.FreshBooks.DotNet.Api.TimeEntry.Models;

namespace Vooban.FreshBooks.DotNet.Api.TimeEntry
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class TimeEntryApi : BaseApi<TimeEntryModel>, IFullBasicApi<TimeEntryModel, TimeEntryFilter>
    {
        #region Constantes

        public const string COMMAND_TIMEENTRY_LIST = "time_entry.list";
        public const string COMMAND_TIMEENTRY_GET = "time_entry.get";
        public const string COMMAND_TIMEENTRY_DELETE = "time_entry.delete";
        public const string COMMAND_TIMEENTRY_CREATE = "time_entry.create";
        public const string COMMAND_TIMEENTRY_UPDATE = "time_entry.update";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public TimeEntryApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
            :base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public TimeEntryApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call the <c>staff.get</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="id">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="StaffModel" /> information for the specified <paramref name="id" />
        /// </returns>
        public FreshbooksGetResponse<TimeEntryModel> CallGet(string id)
        {
            return CallGetMethod(COMMAND_TIMEENTRY_GET, p => p.time_entry_id = id, r => TimeEntryModel.FromFreshbooksDynamic(r.response.time_entry));           
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{StaffModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage
        /// </exception>
        public FreshbooksPagedResponse<TimeEntryModel> CallGetList(int page = 1, int itemPerPage = 100)
        {
            return CallGetListMethod(COMMAND_TIMEENTRY_LIST, r => BuildEnumerableFromDynamicResult(r), null, page, itemPerPage);          
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{StaffModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public FreshbooksPagedResponse<TimeEntryModel> CallSearch(TimeEntryFilter template, int page = 1, int itemPerPage = 100)
        {
            return CallSearchMethod(COMMAND_TIMEENTRY_LIST, r => BuildEnumerableFromDynamicResult(r), template, page, itemPerPage);
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<FreshbooksPagedResponse<TimeEntryModel>> CallGetAllPages()
        {
            return CallGetAllPagesMethod(COMMAND_TIMEENTRY_LIST, r => BuildEnumerableFromDynamicResult(r));            
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{StaffModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public IEnumerable<FreshbooksPagedResponse<TimeEntryModel>> CallSearchAll(TimeEntryFilter template, int page = 1, int itemPerPage = 100)
        {
            return CallSearchAllMethod(COMMAND_TIMEENTRY_LIST, r => BuildEnumerableFromDynamicResult(r), template);
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to create the entry in Freshbooks</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        public FreshbooksCreateResponse CallCreate(TimeEntryModel entity)
        {
            return CallCreateMethod(COMMAND_TIMEENTRY_CREATE, entity, id => id.response.time_entry_id);
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        public FreshbooksResponse CallUpdate(TimeEntryModel entity)
        {
            return CallUpdateMethod(COMMAND_TIMEENTRY_UPDATE, entity);
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>delete</c> operation using an empty Id
        /// or
        /// Cannot call the <c>delete</c> operation using an empty Id identifier</exception>
        public FreshbooksResponse CallDelete(TimeEntryModel entity)
        {
            return CallDeleteMethod(COMMAND_TIMEENTRY_DELETE, entity);
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="id">The identifier of the time entry to delete.</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>delete</c> operation using an empty Id
        /// or
        /// Cannot call the <c>delete</c> operation using an empty Id identifier</exception>
        public FreshbooksResponse CallDelete(string id)
        {
            return CallDeleteMethod(COMMAND_TIMEENTRY_DELETE, "time_entry_id", id);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Build an <see cref="IEnumerable{T}"/> from the dynamic Freshbooks response.
        /// </summary>
        /// <param name="resultList">The Freshbooks response as a dynamic object</param>
        /// <returns>An <see cref="IEnumerable{StaffModel}"/> based on the Freshbooks response.</returns>
        private static IEnumerable<TimeEntryModel> BuildEnumerableFromDynamicResult(dynamic resultList)
        {
            foreach (var item in resultList.response.time_entries.time_entry)
                yield return TimeEntryModel.FromFreshbooksDynamic(item);
        }

        #endregion
    }

}