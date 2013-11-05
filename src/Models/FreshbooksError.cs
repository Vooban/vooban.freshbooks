namespace FreshBooks.Api.Models
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1} [Field : {2}", ErrorCode, ErrorMessage, ErrorField);
        }
    }
}