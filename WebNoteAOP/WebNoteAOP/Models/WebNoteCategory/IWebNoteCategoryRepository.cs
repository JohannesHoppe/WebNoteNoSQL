namespace WebNoteAOP.Models.WebNoteCategory
{
    using System.Collections.Generic;

    /// <summary>
    /// Sample interface
    /// </summary>
    public interface IWebNoteCategoryRepository
    {
        /// <summary>
        /// Gets all categories for one note.
        /// </summary>
        IEnumerable<Category> GetCategories(int noteId);

        /// <summary>
        /// Gets all available categories.
        /// </summary>
        IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// First: Deletes all old relations between one note and its categories
        /// Second: adds the new relations
        /// </summary>
        void UpdateRelation(int noteId, int[] newCategories);

        /// <summary>
        /// Deletes all old relations between one note and its categories
        /// </summary>
        void DeleteAllRelations(int noteId);
    }
}