namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// This class represents the base model for all Freshbooks response
    /// </summary>
    public class FreshbooksResponse
    {
        /// <summary>
        /// Get the Freshbooks status of the requested operation
        /// </summary>
        public bool Success
        {
            get { return Status == "ok"; }
        }

        /// <summary>
        /// Gets the real status information received from Freshbooks.
        /// </summary>
        /// <remarks>Whent his field is != 'ok' the error property is filled with information</remarks>
        public string Status { get; internal set; }

        /// <summary>
        /// Gets the error information when the <see cref="Success"/> property is set to false
        /// </summary>
        public FreshbooksError Error { get; internal set; }
    }
}
