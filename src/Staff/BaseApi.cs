using System;
using System.Collections.Generic;
using HastyAPI;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api.Staff
{
    public abstract class BaseApi<T> where T : FreshbooksModel
    {
        #region Private members

        /// <summary>
        /// This holds the Freshbooks clients that will be used to communicate with Freshbooks
        /// </summary>
        private readonly Lazy<HastyAPI.FreshBooks.FreshBooks> _freshbooks;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi{T}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        protected BaseApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _freshbooks = freshbooks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApi{T}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        protected BaseApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(()=>freshbooks);
        }

        #endregion

        #region Properties

        protected HastyAPI.FreshBooks.FreshBooks FreshbooksClient { get { return _freshbooks.Value; } }

        #endregion

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="apiName">Name of the method.</param>
        /// <param name="resultBuilder">The result builder used to create the resulting object.</param>
        /// <returns>
        /// The <see cref="T" /> information from the call
        /// </returns>
        protected FreshbooksGetResponse<T> CallGetMethod(string apiName, Func<dynamic, T> resultBuilder)
        {
            var callResult = FreshbooksClient.Call(apiName);

            var response = FreshbooksConvert.ToResponse<T>(callResult);

            return (FreshbooksGetResponse<T>)response.WithResult(resultBuilder(callResult));
        }

        /// <summary>
        /// Call the <c>staff.get</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="apiName">Name of the method.</param>
        /// <param name="parameterBuilder">The parameter builder used to create the dynamic parameters to pass on to Freshbooks.</param>
        /// <param name="resultBuilder">The result builder.</param>
        /// <returns>
        /// The <see cref="T" /> information for the specified parameters
        /// </returns>
        protected FreshbooksGetResponse<T> CallGetMethod(string apiName, Action<dynamic> parameterBuilder, Func<dynamic, T> resultBuilder)
        {
            var callResult = FreshbooksClient.Call(apiName, parameterBuilder);

            var response = FreshbooksConvert.ToResponse<T>(callResult);

            return (FreshbooksGetResponse<T>)response.WithResult(resultBuilder(callResult));
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="apiName">Name of the method to call on Freshbooks.</param>
        /// <param name="resultBuilder">The result builder used to build the resulting object.</param>
        /// <param name="parameterBuilder">The parameter builder used to build the query.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{T}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        protected FreshbooksPagedResponse<T> CallGetListMethod(string apiName, Func<dynamic, IEnumerable<T>> resultBuilder, Action<dynamic> parameterBuilder= null, int page = 1, int itemPerPage = 100)
        {
            if (itemPerPage < 1)
                throw new ArgumentException("Please ask for at least 1 item per page otherwise this call is irrelevant.", "itemPerPage");

            if (itemPerPage > 100)
                throw new ArgumentException("The max number of items per page supported by Freshbooks is 100.", "itemPerPage");

            dynamic parameter = new FriendlyDynamic();

            if (parameterBuilder != null)
                parameterBuilder(parameter);

            parameter.page = page;
            parameter.per_page = itemPerPage;

// ReSharper disable RedundantAssignment
            var callListResult = FreshbooksClient.Call(apiName, p=> p = parameter);
// ReSharper restore RedundantAssignment

            var response = (FreshbooksPagedResponse<T>)FreshbooksConvert.ToPagedResponse<T>(callListResult);

            return response.WithResult((IEnumerable<T>)resultBuilder(callListResult));
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        protected IEnumerable<FreshbooksPagedResponse<T>> CallGetAllPagesMethod(string apiName, Func<dynamic, IEnumerable<T>> resultBuilder)
        {
            var result = new List<FreshbooksPagedResponse<T>>();
            var response = CallGetListMethod(apiName, resultBuilder);
            if (response.Status)
            {
                // Add items obtained from the first page
                result.Add(response);

                // Add items for remaining pages                
                for (int i = 2; i <= response.TotalPages; i++)
                {
                    response = CallGetListMethod(apiName, resultBuilder, null, i);
                    result.Add(response);
                }
            }
            else
                throw new InvalidProgramException(string.Format("Freshbooks API failed with status code : {0}", response.Status));

            return result;
        }
    }
}