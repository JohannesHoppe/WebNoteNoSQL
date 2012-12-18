namespace WebNoteAOP.Models
{
    using System.Collections.Generic;

    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    public interface IWebNoteService
    {
        /// <summary>
        /// Gets all notes with category.
        /// </summary>
        IEnumerable<NoteWithCategories> ReadAll();

        /// <summary>
        /// Gets one note and its categories
        /// </summary>
        NoteWithCategories Read(int id);

        /// <summary>
        /// Creates a new note and adds relations to the categories
        /// </summary>
        void Create(Note noteToAdd, int[] newCategories);

        /// <summary>
        /// Update a note and its categories
        /// </summary>
        NoteWithCategories Update(Note noteToEdit, int[] newCategories);

        /// <remarks>
        /// *** NOTE ***
        /// Ups?! We are deleting the note but we do not delete it's realations to the category...
        /// But this is still ok, since we use an EventBroker
        /// </remarks>
        /// <summary>
        /// Deletes one Note (and the relations to the categories)
        /// </summary>
        void Delete(int id);

        /// <summary>
        /// Gets all available categories.
        /// </summary>
        IEnumerable<Category> GetAllCategories();
    }
}