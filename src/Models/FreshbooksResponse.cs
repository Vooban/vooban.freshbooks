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
        public bool Status { get; internal set; }
    }
}
