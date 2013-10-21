using System;
using System.Collections.Generic;

namespace Vooban.FreshBooks.DotNet.Api.Models
{
    public class FreshbooksPagedResponse : FreshbooksResponse
    {
        #region Properties

        /// <summary>
        /// Gets the page number returned by this response
        /// </summary>
        public int Page { get; internal set; }

        /// <summary>
        /// Gets the number of items per page
        /// </summary>
        public int ItemPerPage { get; internal set; }

        /// <summary>
        /// Get the total number of pages available in Freshbooks
        /// </summary>
        public int TotalPages { get; internal set; }

        /// <summary>
        /// Gets the total number of items available in Freshbooks
        /// </summary>
        public int TotalItems { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksPagedResponse"/> class.
        /// </summary>
        /// <param name="inner">The base response object from which we should copy all properties</param>
        public FreshbooksPagedResponse(FreshbooksPagedResponse inner)
        {           
            Status = inner.Status;
            Page = inner.Page;
            ItemPerPage = inner.ItemPerPage;
            TotalPages = inner.TotalPages;
            TotalItems = inner.TotalItems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksPagedResponse"/> class.
        /// </summary>
        public FreshbooksPagedResponse(){ }

        #endregion
    }

    public class FreshbooksPagedResponse<T> : FreshbooksPagedResponse
    {
        #region Properties

        /// <summary>
        /// Gets the list of results associated with the current page
        /// </summary>
        public IEnumerable<T> Result { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksPagedResponse{T}"/> class.
        /// </summary>
        public FreshbooksPagedResponse() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksPagedResponse{T}"/> class.
        /// </summary>
        /// <param name="inner">The base response object from which we should copy all properties</param>
        public FreshbooksPagedResponse(FreshbooksPagedResponse inner) : base(inner)
        {            
        }

        #endregion

        #region Fluent Methods

        /// <summary>
        /// Appends the result of the function to the result property and return this instance
        /// </summary>
        /// <param name="resultBuilder">The result builder used to get the <see cref="IEnumerable{T}"/>.</param>
        /// <returns>The current isntance with the Result properly filled</returns>
        public FreshbooksPagedResponse<T> WithResult(Func<IEnumerable<T>> resultBuilder)
        {
            Result = resultBuilder();
            return this;
        }

        /// <summary>
        /// Appends the result of the function to the result property and return this instance
        /// </summary>
        /// <param name="result">The result that will be used to populate the <see cref="Result"/> property.</param>
        /// <returns>The current isntance with the Result properly filled</returns>
        public FreshbooksPagedResponse<T> WithResult(IEnumerable<T> result)
        {
            Result = result;
            return this;
        }

        #endregion
    }
}