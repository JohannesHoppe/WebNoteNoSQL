namespace WebNoteAOP.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    /// <summary>
    /// Combines a Note with at list of categories
    /// </summary>
    public sealed class NoteWithCategories : Note
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteWithCategories"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "default values are allowed by design")]
        public NoteWithCategories(Note note, IEnumerable<Category> categories)
        {
            this.NoteId = note.NoteId;
            this.Title = note.Title;
            this.Message = note.Message;
            this.Added = note.Added;
            this.Categories = categories;
        }

        /// <summary>
        /// Gets the associated categories.
        /// </summary>
        public IEnumerable<Category> Categories { get; private set; }
    }
}