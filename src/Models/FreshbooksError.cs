namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// Class providing information about Freshbooks API call errors
    /// </summary>
    public class FreshbooksError
    {
        /// <summary>
        /// Gets or set the error message returned by Freshbooks
        /// </summary>
        public string ErrorMessage { get; internal set; }

        /// <summary>
        /// Gets or set the error code returned by Freshbooks
        /// </summary>
        public string ErrorCode { get; internal set; }

        /// <summary>
        /// Gets or set the field in error
        /// </summary>
        public string ErrorField { get; internal set; }

    }
}