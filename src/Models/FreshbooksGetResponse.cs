namespace FreshBooks.Api.Models
{
    /// <summary>
    /// Freshbooks response for <c>GET</c> operations on the Freshbooks API
    /// </summary>
    /// <typeparam name="T">The type of item that will be stored in the <see cref="Result"/> property of this instance</typeparam>
    public class FreshbooksGetResponse<T> : FreshbooksResponse
    {
        #region Properties

        /// <summary>
        /// Gets the operation results
        /// </summary>
        public T Result { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksGetResponse{T}"/> class.
        /// </summary>
        public FreshbooksGetResponse()
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksGetResponse{T}"/> class.
        /// </summary>
        /// <param name="inner">The response from which this response will be initialized.</param>
        public FreshbooksGetResponse(FreshbooksGetResponse<T> inner)
            : base(inner)
        {
            Result = inner.Result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksGetResponse{T}"/> class.
        /// </summary>
        /// <param name="inner">The response from which this response will be initialized.</param>
        public FreshbooksGetResponse(FreshbooksResponse inner) 
            :base(inner)
        { }

        #endregion

        #region Fluent API

        /// <summary>
        /// Appends the result of the function to the result property and return this instance
        /// </summary>
        /// <param name="result">The result that will be put in the result property.</param>
        /// <returns>The current isntance with the Result properly filled</returns>
        public FreshbooksGetResponse<T> WithResult(T result)
        {
            Result = result;
            return this;
        }

        #endregion
    }
}