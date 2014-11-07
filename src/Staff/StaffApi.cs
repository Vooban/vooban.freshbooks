using System;
using System.Collections.Generic;
using Vooban.FreshBooks.Models;
using Vooban.FreshBooks.Staff.Models;

namespace Vooban.FreshBooks.Staff
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class StaffApi : IStaffApi
    {
        #region Private Classes

        public class StaffApiOptions : GenericApiOptions<StaffModel>
        {
            #region Constantes

            public const string COMMAND_STAFF_LIST = "staff.list";
            public const string COMMAND_STAFF_CURRENT = "staff.current";
            public const string COMMAND_STAFF_GET = "staff.get";

            #endregion

            #region Properties

            /// <summary>
            /// Gets the name of the id property used with this entity
            /// </summary>
            public override string IdProperty
            {
                get { return "staff_id"; }
            }

            /// <summary>
            /// Gets the Freshbooks delete command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string DeleteCommand
            {
                get { return null; }
            }

            /// <summary>
            /// Gets the Freshbooks update command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string UpdateCommand
            {
                get { return null; }
            }

            /// <summary>
            /// Gets the Freshbooks create command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string CreateCommand
            {
                get { return null; }
            }

            /// <summary>
            /// Gets the Freshbooks get command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string GetCommand
            {
                get { return COMMAND_STAFF_GET; }
            }

            /// <summary>
            /// Gets the Freshbooks list command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string ListCommand
            {
                get { return COMMAND_STAFF_LIST; }
            }

            /// <summary>
            /// Creates an entity of type <see cref="StaffModel"/> from the Freshbooks dynamic response
            /// </summary>
            /// <returns>The fully loaded entity</returns>
            public override Func<dynamic, StaffModel> FromDynamicModel
            {
                get
                {
                    return d => StaffModel.FromFreshbooksDynamic(d.response.staff);
                }
            }

            /// <summary>
            /// Enumerates over the list response of the Freshbooks API
            /// </summary>
            /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
            public override Func<dynamic, IEnumerable<StaffModel>> BuildEnumerableFromDynamicResult
            {
                get
                {
                    return resultList =>
                    {
                        var result = new List<StaffModel>();
                        
                        foreach (var staffMember in resultList.response.staff_members.member)
                            result.Add(StaffModel.FromFreshbooksDynamic(staffMember));

                        return result;
                    };
                }
            }

            #endregion
        }

        #endregion

        #region Private Members

        private readonly GenericApi<StaffModel, StaffFilter> _api;
        private readonly StaffApiOptions _options;

        #endregion

        #region Constructors

        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _options = new StaffApiOptions();
            _api = new GenericApi<StaffModel, StaffFilter>(freshbooks, _options);
        }

        public StaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _options = new StaffApiOptions();
            _api = new GenericApi<StaffModel, StaffFilter>(freshbooks, _options);
        }

        #endregion       

        #region Specialities

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <returns>The <see cref="StaffModel"/> information of the client used to communicate with Freshbooks</returns>
        public StaffModel CallGetCurrent()
        {
            return _api.CallGetMethod(StaffApiOptions.COMMAND_STAFF_CURRENT, p => _options.FromDynamicModel(p));
        }

        #endregion

        #region IReadOnlyBasicApi

        /// <summary>
        /// Call the api.get method on the Freshbooks API.
        /// </summary>
        /// <param name="id">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="T" /> information for the specified <paramref name="id" />
        /// </returns>
        public StaffModel Get(int id)
        {
            return _api.Get(id);
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API.
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
        public FreshbooksPagedResponse<StaffModel> GetList(int page = 1, int itemPerPage = 100)
        {
            return _api.GetList(page, itemPerPage);
        }

        /// <summary>
        /// Get all the items available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the api.list method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<StaffModel> GetAllPages()
        {
            return _api.GetAllPages();
        }

        #endregion
    }
}