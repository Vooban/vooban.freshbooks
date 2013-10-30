using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    /// <summary>
    /// Generic API implementing all the common features of a Freshbooks API
    /// </summary>
    /// <typeparam name="T">The type of model managed by the class instance</typeparam>
    /// <typeparam name="TF">The type of filter used to search Freshbooks for this API</typeparam>
    /// <remarks>The child classes can return null to command the API does not implement</remarks>
    public abstract class GenericApi<T, TF> :
        BaseApi<T>, IFullBasicApi<T, TF>
        where T : FreshbooksModel
        where TF : FreshbooksFilter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApi{T, TF}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        protected GenericApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks) 
            : base(freshbooks)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApi{T, TF}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        protected GenericApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        { }

        #endregion

        #region IFullBasicApi

        /// <summary>
        /// Call the api.get method on the Freshbooks API.
        /// </summary>
        /// <param name="id">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="T" /> information for the specified <paramref name="id" />
        /// </returns>
        public FreshbooksGetResponse<T> CallGet(string id)
        {
            if (!string.IsNullOrEmpty(GetCommand))
                return CallGetMethod(GetCommand, p => ((IDictionary<string, object>)p)[IdProperty] = id, r => FromDynamicModel(r));

            throw new NotSupportedException("The [get] operation is not supported");
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
        public FreshbooksPagedResponse<T> CallGetList(int page = 1, int itemPerPage = 100)
        {
            if (!string.IsNullOrEmpty(ListCommand))
                return CallGetListMethod(ListCommand, r => BuildEnumerableFromDynamicResult(r), null, page, itemPerPage);

            throw new NotSupportedException("The [list] operation is not supported");
        }

        /// <summary>
        /// Get all the items available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the api.list method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<FreshbooksPagedResponse<T>> CallGetAllPages()
        {
            if (!string.IsNullOrEmpty(ListCommand))
                return CallGetAllPagesMethod(ListCommand, r => BuildEnumerableFromDynamicResult(r));

            throw new NotSupportedException("The [list] operation is not supported");
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to create the entry in Freshbooks</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        public FreshbooksCreateResponse CallCreate(T entity)
        {
            if (!string.IsNullOrEmpty(CreateCommand))
                return CallCreateMethod(CreateCommand, entity, createResponse => (string) ((IDictionary<string, object>) createResponse.response)[IdProperty]);

            throw new NotSupportedException("The [create] operation is not supported");
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        public FreshbooksResponse CallUpdate(T entity)
        {
            if (!string.IsNullOrEmpty(UpdateCommand))
                return CallUpdateMethod(UpdateCommand, entity);

            throw new NotSupportedException("The [update] operation is not supported");
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
        public FreshbooksResponse CallDelete(T entity)
        {
            if (!string.IsNullOrEmpty(DeleteCommand))
                return CallDeleteMethod(DeleteCommand, IdProperty, entity.Id);

            throw new NotSupportedException("The [delete] operation is not supported");
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
            if (!string.IsNullOrEmpty(DeleteCommand))
                return CallDeleteMethod(DeleteCommand, IdProperty, id);

            throw new NotSupportedException("The [delete] operation is not supported");
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
        public FreshbooksPagedResponse<T> CallSearch(TF template, int page = 1, int itemPerPage = 100)
        {
            if (!string.IsNullOrEmpty(ListCommand))
                return CallSearchMethod(ListCommand, r => BuildEnumerableFromDynamicResult(r), template, page, itemPerPage);

            throw new NotSupportedException("The [search] operation is not supported");
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
        public IEnumerable<FreshbooksPagedResponse<T>> CallSearchAll(TF template, int page = 1, int itemPerPage = 100)
        {
            if (!string.IsNullOrEmpty(ListCommand))
                return CallSearchAllMethod(ListCommand, r => BuildEnumerableFromDynamicResult(r), template);

            throw new NotSupportedException("The [search all] operation is not supported");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the id property used with this entity
        /// </summary>
        protected abstract string IdProperty { get; }

        /// <summary>
        /// Gets the Freshbooks delete command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected abstract string DeleteCommand { get; }

        /// <summary>
        /// Gets the Freshbooks update command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected abstract string UpdateCommand { get; }

        /// <summary>
        /// Gets the Freshbooks create command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected abstract string CreateCommand { get; }

        /// <summary>
        /// Gets the Freshbooks get command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected abstract string GetCommand { get; }

        /// <summary>
        /// Gets the Freshbooks list command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected abstract string ListCommand { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an entity of type <see cref="T"/> from the Freshbooks dynamic response
        /// </summary>
        /// <param name="response">The response received from Freshbooks</param>
        /// <returns>The fully loaded entity</returns>
        protected abstract T FromDynamicModel(dynamic response);

        /// <summary>
        /// Enumerates over the list response of the Freshbooks API
        /// </summary>
        /// <param name="response">The Freshbooks response</param>
        /// <returns>An <see cref="IEnumerable{T}"/> all loaded correctly</returns>
        protected abstract IEnumerable<T> BuildEnumerableFromDynamicResult(dynamic response);

        #endregion
    }
}