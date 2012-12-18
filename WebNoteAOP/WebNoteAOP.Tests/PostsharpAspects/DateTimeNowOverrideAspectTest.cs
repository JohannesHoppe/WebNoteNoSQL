namespace WebNoteAOP.Tests.PostsharpAspects
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using global::PostsharpAspects;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNote.WebNoteUnitTest;

    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// Test for the aspect DateTimeNowOverride
    /// </summary>
    [TestClass]
    public class DateTimeNowOverrideAspectTest : WebNoteBaseRepositoryTest
    {
        /// <summary>
        /// Test if it can override System.DateTime.Now,
        /// which is used in <see cref="WebNoteAOP.Models.WebNote" />.AddNote
        /// </summary>
        [TestMethod]
        public void CanOverrideDateTimeNow()
        {
            // Arrange
            Note expected = new Note
                {
                    NoteId = 4, 
                    Added = new DateTime(2011, 2, 5)
                };
            NewDateTime.DateTime = new DateTime(2011, 2, 5);
            
            // Act
            ////UNDONE: DateTimeNowOverrideAspectTest --> AddNote will use a overridden time
            Repository.AddNote(new Note());
            Note actual = (from n in Context.Notes select n).Last();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanRestoreDefaultBehaviour()
        {
            // Arrange
            Note expected = new Note
            {
                NoteId = 4,
                Added = new DateTime(2011, 2, 5)
            };
            NewDateTime.DateTime = DateTime.MinValue;

            // Act
            Repository.AddNote(new Note());
            Note actual = (from n in Context.Notes select n).Last();

            // Assert
            Assert.AreNotEqual(expected, actual);
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
