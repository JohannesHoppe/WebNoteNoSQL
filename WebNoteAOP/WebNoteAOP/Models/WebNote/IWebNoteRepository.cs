namespace WebNoteAOP.Models.WebNote
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for WebNoteRepository
    /// </summary>
    public interface IWebNoteRepository
    {
        /// <summary>
        /// Gets all notes from DB
        /// </summary>
        /// <returns>all notes from DB</returns>
        IEnumerable<Note> GetAllNotes();

        /// <summary>
        /// Gets one note from DB
        /// </summary>
        /// <param name="id">The id of the note to show.</param>
        /// <returns>notes from DB</returns>
        Note GetNote(int id);

        /// <summary>
        /// Adds a new note to the DB.
        /// </summary>
        /// <param name="noteToAdd">The note to add.</param>
        int AddNote(Note noteToAdd);

        /// <summary>
        /// Edits one note note.
        /// </summary>
        /// <param name="noteData">note data.</param>
        void EditNote(Note noteData);

        /// <summary>
        /// Deletes one note note.
        /// </summary>
        /// <param name="id">The id of the note to delete.</param>
        void DeleteNote(int id);
    }
}