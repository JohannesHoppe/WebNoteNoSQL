namespace WebNoteAOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;

    using Microsoft.Practices.Unity;

    using PostsharpAspects;
    using PostsharpAspects.Validation;

    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    /// <summary>
    /// A sample service layer
    /// </summary>
    /// <remarks>
    /// *** NOTE ***
    /// 1. Here we have a big bottleneck which is caused by a poor API
    ///    A perfect spot to enable caching!
    /// 2. throwing base Exceptions is done for the demo only (ugly code)
    /// </remarks>
    //[ValidatorIntroduceAspect]
    public class WebNoteService : IWebNoteService
    {
        /// <summary>
        /// Gets or sets the required WebNoteRepository
        /// </summary>
        [Dependency]
        public IWebNoteRepository WebNoteRepository { private get; set; }

        /// <summary>
        /// Gets or sets the required WebNoteCategoryRepository
        /// </summary>
        [Dependency]
        public IWebNoteCategoryRepository WebNoteCategoryRepository { private get; set; }

        /// <summary>
        /// Gets all notes with category.
        /// </summary>
        ////[CacheGetAspect]
        public IEnumerable<NoteWithCategories> ReadAll()
        {
            IEnumerable<Note> simpleList = this.WebNoteRepository.GetAllNotes();

            // this could be slow, we need a cache here! --> ugly code!
            List<NoteWithCategories> extendedList = simpleList.Select(
                note => new NoteWithCategories(
                    note,
                    this.WebNoteCategoryRepository.GetCategories(note.NoteId))).ToList();

            ////Thread.Sleep(3000); // make it very slow
            return extendedList;
        }

        /// <summary>
        /// Gets one note and its categories
        /// </summary>
        public NoteWithCategories Read(int id)
        {
            // these are two database calls (this could be a little bit slower)
            NoteWithCategories noteWithCategories = new NoteWithCategories(
                this.WebNoteRepository.GetNote(id),
                this.WebNoteCategoryRepository.GetCategories(id));

            ////Thread.Sleep(3000); // make it very slow            
            return noteWithCategories;
        }

        /// <summary>
        /// Creates a new note and adds relations to the categories
        /// </summary>
        public void Create(Note noteToAdd, int[] newCategories)
        {
            int newNoteId = this.WebNoteRepository.AddNote(noteToAdd);
            this.WebNoteCategoryRepository.UpdateRelation(newNoteId, newCategories);
        }

        /// <summary>
        /// Update a note and its categories
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Bad code by intention!")]
        public NoteWithCategories Update(Note noteToEdit, int[] newCategories)
        {
            if (noteToEdit == null ||
                string.IsNullOrEmpty(noteToEdit.Title) ||
                noteToEdit.NoteId == 0)
            {
                throw new Exception("Wrong data!"); // ugly code!
            }
            
            this.WebNoteRepository.EditNote(noteToEdit);
            this.WebNoteCategoryRepository.UpdateRelation(noteToEdit.NoteId, newCategories);

            return this.Read(noteToEdit.NoteId);
        }

        /// <remarks>
        /// *** NOTE ***
        /// Ups?! We are deleting the note but we do not delete it's realations to the category...
        /// But this is still ok, since we use an EventBroker
        /// </remarks>
        /// <summary>
        /// Deletes one Note (and the relations to the categories)
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Bad code by intention!")]
        public void Delete(int id)
        {
            if (id == 0)
            {
                throw new Exception("Null!"); // ugly code!
            }

            this.WebNoteCategoryRepository.DeleteAllRelations(id);
            this.WebNoteRepository.DeleteNote(id);
        }

        /// <summary>
        /// Gets all available categories.
        /// </summary>
        public IEnumerable<Category> GetAllCategories()
        {
            return this.WebNoteCategoryRepository.GetAllCategories();
        }
    }
}