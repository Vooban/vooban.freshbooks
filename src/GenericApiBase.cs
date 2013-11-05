using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using HastyAPI;
using FreshBooks.Api.Models;

namespace FreshBooks.Api
{
    /// <summary>
    /// Base API class allowing an easy development of Freshbooks API ressources
    /// </summary>
    /// <typeparam name="T">The type of model manipulated by this class</typeparam>
    public abstract class GenericApiBase<T> 
        where T : FreshbooksModel
    {
        #region Private members

        /// <summary>
        /// This holds the Freshbooks clients that will be used to communicate with Freshbooks
        /// </summary>
        private readonly Lazy<HastyAPI.FreshBooks.FreshBooks> _freshbooks;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApiBase{T}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        protected GenericApiBase(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _freshbooks = freshbooks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApiBase{T}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        protected GenericApiBase(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(()=>freshbooks);
        }

        #endregion

        #region Properties

        protected HastyAPI.FreshBooks.FreshBooks FreshbooksClient { get { return _freshbooks.Value; } }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="apiName">The name of the API to call</param>
        /// <param name="entity">Then entity used to create the entry</param>
        /// <param name="idBuilder">The function allowing one to return the Id from the dynamic response</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        protected string CallCreateMethod(string apiName, T entity, Func<dynamic, string> idBuilder)
        {
            if(entity.Id != null)
                throw new InvalidOperationException("Cannot call the <create> operation using an entity with an Id");

            var callResult = FreshbooksClient.Call(apiName, a => ((IDictionary<string, object>)a).Add(entity.FreshbooksEntityName, entity.ToFreshbooksDynamic()));

            var response = (FreshbooksCreateResponse)FreshbooksConvert.ToCreateResponse(callResult);

            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());

            var result = (FreshbooksCreateResponse)response.WithResult(idBuilder(callResult));

            return result.Id;
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="apiName">The name of the API to call</param>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        protected void CallUpdateMethod(string apiName, T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                throw new InvalidOperationException("Cannot call the <update> operation using an entity without an Id");

            var callResult = FreshbooksClient.Call(apiName, a => ((IDictionary<string, object>)a).Add(entity.FreshbooksEntityName, entity.ToFreshbooksDynamic()));

            var response = (FreshbooksResponse)FreshbooksConvert.ToCreateResponse(callResult);
            
            if (!response.Success)            
                throw new InvalidOperationException(response.Error.ToString());
        }


        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="apiName">The name of the API to call</param>
        /// <param name="idName">Name of field that act as an id.</param>
        /// <param name="idValue">The value to pass into the id parameter.</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot call the <c>delete</c> operation using an empty Id
        /// or
        /// Cannot call the <c>delete</c> operation using an empty Id identifier
        /// </exception>
        protected void CallDeleteMethod(string apiName, string idName, string idValue)
        {
            if (string.IsNullOrEmpty(idValue))
                throw new InvalidOperationException("Cannot call the <delete> operation using an empty Id");

            if (string.IsNullOrEmpty(idName))
                throw new InvalidOperationException("Cannot call the <delete> operation using an empty Id identifier");

            var callResult = FreshbooksClient.Call(apiName, a => ((IDictionary<string, object>)a)[idName] = idValue);

            var response = (FreshbooksResponse)FreshbooksConvert.ToCreateResponse(callResult);

            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());
        }

        /// <summary>
        /// Call the method corresponsding to the <c>apiName</c> on the Freshbooks API.
        /// </summary>
        /// <param name="apiName">Name of the method.</param>
        /// <param name="resultBuilder">The result builder used to create the resulting object.</param>
        /// <returns>
        /// The <see cref="T" /> information from the call
        /// </returns>
        public T CallGetMethod(string apiName, Func<dynamic, T> resultBuilder)
        {
            var callResult = FreshbooksClient.Call(apiName);

            var response = FreshbooksConvert.ToGetResponse<T>(callResult);

            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());

            var result = (FreshbooksGetResponse<T>)response.WithResult(resultBuilder(callResult));

            return result.Result;            
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
        public T CallGetMethod(string apiName, Action<dynamic> parameterBuilder, Func<dynamic, T> resultBuilder)
        {
            var callResult = FreshbooksClient.Call(apiName, parameterBuilder);

            var response = FreshbooksConvert.ToGetResponse<T>(callResult);

            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());

            var result = (FreshbooksGetResponse<T>)response.WithResult(resultBuilder(callResult));

            return result.Result;
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

            dynamic paging = new ExpandoObject();
            paging.page = page;
            paging.per_page = itemPerPage;

            Action<dynamic> pbuilder = p => Combine(p, parameter, paging);
           
            // ReSharper disable RedundantAssignment
            var callListResult = FreshbooksClient.Call(apiName, pbuilder);
            // ReSharper restore RedundantAssignment

            var response = (FreshbooksPagedResponse<T>)FreshbooksConvert.ToPagedResponse<T>(callListResult);
            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());

            var result = response.WithResult((IEnumerable<T>)resultBuilder(callListResult));
            return result;
        }

      /// <summary>
        /// Allows one to search based on object template
        /// </summary>
        /// <param name="apiName">The name of the API to call</param>
        /// <param name="resultBuilder">The result builder use to create the resulting <see cref="IEnumerable{T}"/></param>
        /// <param name="template">The template onto which the query will be created</param>
        /// <param name="page">The page requested</param>
        /// <param name="itemPerPage">The number of item per page requestes</param>
        /// <returns></returns>
        protected FreshbooksPagedResponse<T> CallSearchMethod<TF>(string apiName, Func<dynamic, IEnumerable<T>> resultBuilder, TF template, int page = 1, int itemPerPage = 100) where TF : FreshbooksFilter
        {
            if (itemPerPage < 1)
                throw new ArgumentException("Please ask for at least 1 item per page otherwise this call is irrelevant.", "itemPerPage");

            if (itemPerPage > 100)
                throw new ArgumentException("The max number of items per page supported by Freshbooks is 100.", "itemPerPage");

            dynamic paging = new ExpandoObject();
            paging.page = page;
            paging.per_page = itemPerPage;

            Action<dynamic> parameterBuilder = p=> Combine(p, template.ToFreshbooksDynamic(), paging);
           
            // ReSharper disable RedundantAssignment
            var callListResult = FreshbooksClient.Call(apiName, parameterBuilder);

            // ReSharper restore RedundantAssignment

            var response = (FreshbooksPagedResponse<T>)FreshbooksConvert.ToPagedResponse<T>(callListResult);
            if (!response.Success)
                throw new InvalidOperationException(response.Error.ToString());

            var result = response.WithResult((IEnumerable<T>)resultBuilder(callListResult));
            return result;
       }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        protected IEnumerable<T> CallGetAllPagesMethod(string apiName, Func<dynamic, IEnumerable<T>> resultBuilder)
        {
            var result = new List<T>();
            var response = CallGetListMethod(apiName, resultBuilder);
            if (response.Success)
            {
                // Add items obtained from the first page
                result.AddRange(response.Result);

                // Add items for remaining pages                
                for (var i = 2; i <= response.TotalPages; i++)
                {
                    response = CallGetListMethod(apiName, resultBuilder, null, i);
                    if (!response.Success)
                        throw new InvalidOperationException(response.Error.ToString());

                    result.AddRange(response.Result);
                }
         }
            else
                throw new InvalidOperationException(response.Error.ToString());         

            return result;
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        protected IEnumerable<T> CallSearchAllMethod<TF>(string apiName, Func<dynamic, IEnumerable<T>> resultBuilder, TF template) where TF : FreshbooksFilter
        {
            var result = new List<T>();
            var response = CallSearchMethod(apiName, resultBuilder, template);
            if (response.Success)
            {
                // Add items obtained from the first page
                result.AddRange(response.Result);

                // Add items for remaining pages                
                for (int i = 2; i <= response.TotalPages; i++)
                {
                    response = CallSearchMethod(apiName, resultBuilder, template, i);
                    if (!response.Success)
                        throw new InvalidOperationException(response.Error.ToString());

                    result.AddRange(response.Result);
                }
            }
            else
                throw new InvalidOperationException(response.Error.ToString());         

            return result;
        }

        #endregion

        #region Private Methods

        static dynamic Combine(dynamic item1, dynamic item2, dynamic item3)
        {
            var dictionary1 = (IDictionary<string, object>)item1;
            var dictionary2 = (IDictionary<string, object>)item2;
            var dictionary3 = (IDictionary<string, object>)item3;
           
            foreach (var pair in dictionary1.Concat(dictionary2).Concat(dictionary3))
            {
                dictionary1[pair.Key] = pair.Value;
            }

            return dictionary1;
        }

        #endregion
    }
}