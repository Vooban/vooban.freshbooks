namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// This class represent the base model of all Freshbooks model intances
    /// </summary>
    public abstract class FreshbooksModel : FreshbooksObject
    {
        /// <summary>
        /// The unique identifier of the model
        /// </summary>
        public string Id { get; set; }
    }
}