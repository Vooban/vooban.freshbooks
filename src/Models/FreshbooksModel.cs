namespace Vooban.FreshBooks.Models
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

        /// <summary>
        /// Gets the name of the Freshbooks entity related to this model
        /// </summary>
        public abstract string FreshbooksEntityName { get; }
    }
}