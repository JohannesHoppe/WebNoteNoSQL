namespace WebNoteAOP.Tests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NUnit.Framework;
    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNote.WebNoteUnitTest;
    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// Example test for the WebNoteRepository
    /// </summary>
    [TestClass]
    public class WebNoteRepositoryTest : WebNoteBaseRepositoryTest
    {
        [TestMethod]
        public void GetAllNotesTest()
        {
            // Arrange
            IEnumerable<Note> expected = this.CreateNotesList();

            // Act
            IEnumerable<Note> actual = Repository.GetAllNotes();

            // Assert
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        [TestMethod]
        public void AddNoteTest()
        {
            // Arrange
            int countBefore = this.Context.Notes.Count();

            // Act
            Repository.AddNote(new Note());
            int countAfter = this.Context.Notes.Count();

            // Assert
            Assert.That(countBefore + 1, Is.EqualTo(countAfter));
            Assert.That(this.Context.SavesChanged);
        }

        [TestMethod]
        public void EditNoteTest()
        {
            // Arrange
            Note noteToChange = new Note { NoteId = 2, Title = "Micky", Message = "Maus" };

            // Act
            Repository.EditNote(noteToChange);
            Note changedNote = (from n in this.Context.Notes
                                where n.NoteId == noteToChange.NoteId
                                select n).First();

            // Assert
            Assert.That(changedNote.Title, Is.EquivalentTo(noteToChange.Title));
            Assert.That(changedNote.Message, Is.EquivalentTo(noteToChange.Message));
            Assert.That(this.Context.SavesChanged);
        }

        [TestMethod]
        public void DeleteNoteShouldNotThrowExceptionTest()
        {
            // Arrange
            const int UnknownId = 9999;

            // Act
            TestDelegate deleteAction = () => Repository.DeleteNote(UnknownId);

            // Assert
            Assert.DoesNotThrow(deleteAction);
        }

        [TestMethod]
        public void GetNoteShouldThrowExceptionTest()
        {
            // Arrange
            const int UnknownId = 9999;

            // Act
            TestDelegate getAction = () => Repository.GetNote(UnknownId);

            // Assert
            Assert.Throws<ArgumentException>(getAction);
        }

        [TestMethod]
        public void GetNoteShouldNotCallSaveChangesTest()
        {
            // Arrange
            var expected = new Note
                {
                    NoteId = 3,
                    Title = "Unit",
                    Message = "Test",
                    Added = DateTime.Parse("2010-10-29", CultureInfo.InvariantCulture)
                };

            // Act
            var actual = Repository.GetNote(expected.NoteId);

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(!this.Context.SavesChanged);
        }

        /// <summary>
        /// Returns a collection for the MockObjectSet
        /// </summary>
        /// <returns>three Notes</returns>
        public override List<Note> CreateNotesList()
        {
            DateTime friday = DateTime.Parse("2010-10-29", CultureInfo.InvariantCulture);

            return new List<Note>
                {
                    new Note { NoteId = 1, Title = "Hello", Message = "World", Added = friday },
                    new Note { NoteId = 2, Title = "Foo", Message = "Bar", Added = friday },
                    new Note { NoteId = 3, Title = "Unit", Message = "Test", Added = friday }
                };
        }
    }
}
