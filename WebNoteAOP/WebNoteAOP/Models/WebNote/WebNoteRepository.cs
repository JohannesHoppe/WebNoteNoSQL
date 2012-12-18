#if DEBUG
////UNDONE: DateTimeNowOverrideAspect --> overrides DateTime.Now for UnitTests 
[assembly: PostsharpAspects.DateTimeNowOverrideAspect(
    AttributeTargetAssemblies = "mscorlib",
    AttributeTargetTypes = "System.DateTime")]
#endif

namespace WebNoteAOP.Models.WebNote
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Sample Repository: Publisher
    /// </summary>
    public partial class WebNoteRepository : IWebNoteRepository
    {
        /// <summary>
        /// Gets all notes from DB
        /// </summary>
        /// <returns>all notes from DB</returns>
        public IEnumerable<Note> GetAllNotes()
        {
            return (from n in this.Context.Notes
                    orderby n.NoteId descending
                    select n).ToList();
        }

        /// <summary>
        /// Gets one note from DB
        /// </summary>
        /// <param name="id">The id of the note to show.</param>
        /// <returns>notes from DB</returns>
        public Note GetNote(int id)
        {
            Note noteToShow = (from n in this.Context.Notes
                               where n.NoteId == id
                               select n).FirstOrDefault();

            if (noteToShow == null)
            {
                throw new ArgumentException("A note with the given ID " + id + " was not found!");
            }

            return noteToShow;
        }

        /// <summary>
        /// Adds a new note to the DB.
        /// </summary>
        /// <param name="noteToAdd">The note to add.</param>
        public int AddNote(Note noteToAdd)
        {
            noteToAdd.Added = DateTime.Now;
            this.Context.Notes.AddObject(noteToAdd);
            this.Context.SaveChanges();

            return noteToAdd.NoteId;
        }

        /// <summary>
        /// Edits one note.
        /// </summary>
        /// <param name="noteData">note data.</param>
        public void EditNote(Note noteData)
        {
            Note noteToEdit = this.GetNote(noteData.NoteId);

            if (noteToEdit == null)
            {
                return;
            }

            noteToEdit.Title = noteData.Title;
            noteToEdit.Message = noteData.Message;
            this.Context.SaveChanges();
        }

        /// <summary>
        /// Deletes one note.
        /// </summary>
        /// <param name="id">The id of the note to delete.</param>
        public void DeleteNote(int id)
        {
            Note noteToDelete = (from n in this.Context.Notes
                                 where n.NoteId == id
                                 select n).FirstOrDefault();

            if (noteToDelete == null)
            {
                return;
            }

            this.Context.Notes.DeleteObject(noteToDelete);
            this.Context.SaveChanges();
        }
    }
}