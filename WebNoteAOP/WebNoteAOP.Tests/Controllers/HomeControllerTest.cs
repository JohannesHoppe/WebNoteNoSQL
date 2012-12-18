namespace WebNoteAOP.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NUnit.Framework;

    using WebNoteAOP.Controllers;
    using WebNoteAOP.Models;
    using WebNoteAOP.Models.WebNote;

    using Assert = NUnit.Framework.Assert;

    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexWasChangedTest()
        {
            // Arrange Repository
            Mock<IWebNoteService> service = new Mock<IWebNoteService>();
            service.SetupAllProperties();
            
            // Arrange Controller
            HomeController controller = new HomeController { WebNoteService = service.Object };

            // Act
            ViewResult result = controller.Index() as ViewResult;
            string message = result.ViewData["Message"] as string;

            // Assert
            Assert.AreNotEqual("Welcome to ASP.NET MVC!", message);
        }

        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var expectedList = this.CreateNotesList();

            // Arrange Repository
            Mock<IWebNoteService> service = new Mock<IWebNoteService>();
            service.Setup(r => r.ReadAll()).Returns(expectedList);

            // Arrange Controller
            HomeController controller = new HomeController { WebNoteService = service.Object };

            // Act
            ViewResult result = controller.Index() as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            var returnedList = viewData.Model as List<NoteWithCategories>;

            // Assert
            Assert.That(returnedList, Is.EquivalentTo(expectedList));
        }

        [TestMethod]
        public void IndexShoudlUseRepositoryTest()
        {
            // Arrange
            Mock<IWebNoteService> service = new Mock<IWebNoteService>();
            HomeController controller = new HomeController { WebNoteService = service.Object };

            // Act
            controller.Index();

            // Assert - method called at least once
            service.Verify(r => r.ReadAll(), Times.AtLeastOnce());
        }

        /// <summary>
        /// Returns a collection for the MockObjectSet
        /// </summary>
        /// <returns>three Notes</returns>
        public List<NoteWithCategories> CreateNotesList()
        {
            DateTime friday = DateTime.Parse("2010-10-29", CultureInfo.InvariantCulture);

            var notes = new List<Note>
                {
                    new Note { NoteId = 1, Title = "Hello", Message = "World", Added = friday },
                    new Note { NoteId = 2, Title = "Foo", Message = "Bar", Added = friday },
                    new Note { NoteId = 3, Title = "Unit", Message = "Test", Added = friday }
                };

            List<NoteWithCategories> extendedNotes = notes.Select(note => new NoteWithCategories(note, null)).ToList();

            return extendedNotes;
        }
    }
}

