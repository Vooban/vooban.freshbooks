namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// This class represent the base model of all Freshbooks model intances
    /// </summary>
    public abstract class FreshbooksModel
    {
        /// <summary>
        /// The unique identifier of the model
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Converts this instance to a Freshbooks compatible dynamic instance
        /// </summary>
        /// <returns>The dynamic instance representing the Freshbooks object</returns>
        public abstract dynamic ToFreshbooksDynamic();
    }
}