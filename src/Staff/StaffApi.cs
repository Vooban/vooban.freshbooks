using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;

namespace Vooban.FreshBooks.DotNet.Api.Staff
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class StaffApi : BaseApi<StaffModel>, IReadOnlyBasicApi<StaffModel>
    {
        #region Constantes

        public const string COMMAND_STAFF_LIST = "staff.list";
        public const string COMMAND_STAFF_CURRENT = "staff.current";
        public const string COMMAND_STAFF_GET = "staff.get";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
            :base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public StaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <returns>The <see cref="StaffModel"/> information of the client used to communicate with Freshbooks</returns>
        public FreshbooksGetResponse<StaffModel> CallGetCurrent()
        {
            return CallGetMethod(COMMAND_STAFF_CURRENT, p => StaffModel.FromFreshbooksDynamic(p.response.staff));           
        }

        /// <summary>
        /// Call the <c>staff.get</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="staffId">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="StaffModel" /> information for the specified <paramref name="staffId" />
        /// </returns>
        public FreshbooksGetResponse<StaffModel> CallGet(string staffId)
        {
            return CallGetMethod(COMMAND_STAFF_GET, p => p.staff_id = staffId, r => StaffModel.FromFreshbooksDynamic(r.response.staff));           
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
        public FreshbooksPagedResponse<StaffModel> CallGetList(int page = 1, int itemPerPage = 100)
        {
            return CallGetListMethod(COMMAND_STAFF_LIST, r => BuildEnumerableFromDynamicResult(r), null, page, itemPerPage);          
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API to perform a search
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{T}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public FreshbooksPagedResponse<StaffModel> CallSearch(StaffFilter template, int page = 1, int itemPerPage = 100)
        {
            throw new NotImplementedException("The search mecanism is not supported by the freshbooks server API implementation");
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<FreshbooksPagedResponse<StaffModel>> CallGetAllPages()
        {
            return CallGetAllPagesMethod(COMMAND_STAFF_LIST, r => BuildEnumerableFromDynamicResult(r));            
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API.
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{T}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public IEnumerable<FreshbooksPagedResponse<StaffModel>> CallSearchAll(StaffFilter template, int page = 1, int itemPerPage = 100)
        {
            throw new NotImplementedException("The search mecanism is not supported by the freshbooks server API implementation");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Build an <see cref="IEnumerable{T}"/> from the dynamic Freshbooks response.
        /// </summary>
        /// <param name="resultStaff">The Freshbooks response as a dynamic object</param>
        /// <returns>An <see cref="IEnumerable{StaffModel}"/> based on the Freshbooks response.</returns>
        private static IEnumerable<StaffModel> BuildEnumerableFromDynamicResult(dynamic resultStaff)
        {
            foreach (var staffMember in resultStaff.response.staff_members.member)
                yield return StaffModel.FromFreshbooksDynamic(staffMember);
        }

        #endregion
    }
}