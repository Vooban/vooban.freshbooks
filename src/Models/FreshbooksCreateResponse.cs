namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// Freshbooks response for creation operation
    /// </summary>
    public class FreshbooksCreateResponse : FreshbooksResponse
    {
        #region Properties

        /// <summary>
        /// Gets the identifier returned by the create operation
        /// </summary>
        public string Id { get; internal set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksCreateResponse"/> class.
        /// </summary>
        public FreshbooksCreateResponse()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksCreateResponse"/> class.
        /// </summary>
        /// <param name="inner">The response from which this one will be constructed.</param>
        public FreshbooksCreateResponse(FreshbooksCreateResponse inner)
            : base(inner)
        {
            Id = inner.Id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FreshbooksCreateResponse"/> class.
        /// </summary>
        /// <param name="inner">The response from which this one will be constructed.</param>
        public FreshbooksCreateResponse(FreshbooksResponse inner) 
            : base(inner)
        {}

        #endregion

        #region Fluent API

        /// <summary>
        /// Appends the result of the function to the result property and return this instance
        /// </summary>
        /// <param name="id">The id that will be put in the Id property.</param>
        /// <returns>The current isntance with the Result properly filled</returns>
        public FreshbooksCreateResponse WithResult(string id)
        {
            Id = id;
            return this;
        }

        #endregion
    }
}