using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Linq;
using WebNoteNoSQL.Models;
using WebNoteNoSQL.Models.RavenDb;

namespace WebNoteTests.RavenDbRepositoryTests
{
    /// <summary>
    /// Note: These tests are too long. For demonstration purposes this test reproduces
    /// the IWebNoteRepository interface which is to verbose to begin with!
    /// </summary>
    [TestFixture]
    public class RavenDbRepositoryTests
    {
        [Test]
        public void ShouldReadAll()
        {
            // Arrange
            var note = new NoteWithCategories { Message = "Message" };
            var expectedNotes = new List<NoteWithCategories> { note };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            var query = Substitute.For<IRavenQueryable<NoteWithCategories>>();
                       
            // Indirect Input
            session.Query<NoteWithCategories>().Returns(query);
            query.GetEnumerator().Returns(expectedNotes.GetEnumerator());
            
            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            IEnumerable<NoteWithCategories> notes = repository.ReadAll();

            // Assert
            Assert.That(notes, Is.EquivalentTo(expectedNotes));
            Assert.That(notes.First().Message, Is.EqualTo(note.Message));
        }

        [Test]
        public void ShouldReadOne() 
        {
            // Arrange
            string id = "Id";
            var expectedNote = new NoteWithCategories { Id = id, Message = "Message" };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();

            // Indirect Input
            session.Load<NoteWithCategories>(Arg.Is(id)).Returns(expectedNote);

            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            NoteWithCategories note = repository.Read(id);

            // Assert
            Assert.That(note.Message, Is.EqualTo(note.Message));
        }

        [Test]
        public void ShouldCreateANoteAndAddTheDate()
        {
            // Arrange
            var id = "id";
            var today = DateTime.Now;
            var note = new Note { Id = id, Message = "Message" };
            var categories = new[] { new Category { Name = "Important" } };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            
            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            repository.Create(note, categories);
            
            // Assert / Indirect Output
            session.Received().Store(Arg.Is<Note>(n => n.Id == id && n.Added.Date == today.Date));
            session.Received().SaveChanges();
        }

        [Test]
        public void ShouldUpdateANote()
        {
            // Arrange
            var id = "123";
            var note = new Note { Id = id, Message = "Message" };
            var categories = new[] { new Category { Name = "Important" } };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();

            // System under Test
            var repository = new RavenDbRepository(session);

            repository.Update(note, categories);

            // Assert / Indirect Output
            session.Received().Store(Arg.Is<Note>(n => n.Id == id));
            session.Received().SaveChanges();
        }

        [Test]
        public void ShouldDeleteANote()
        {
            // Arrange
            string id = "id";
            var note = new NoteWithCategories { Id = id, Message = "Message" };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            session.Load<NoteWithCategories>(id).Returns(note);

            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            repository.Delete(id);

            // Assert / Indirect Output
            session.Received().Delete(Arg.Is(note));
            session.Received().SaveChanges();
        }

        [Test]
        public void ShouldLoadCategories()
        {
            // Arrange
            var category = new Category { Name = "Important" };
            var expectedCategories = new List<Category> { category };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            var query = Substitute.For<IRavenQueryable<Category>>();

            // Indirect Input
            session.Query<Category>().Returns(query);
            query.GetEnumerator().Returns(expectedCategories.GetEnumerator());

            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            IEnumerable<Category> categories = repository.GetAllCategories();

            // Assert
            Assert.That(categories, Is.EquivalentTo(expectedCategories));
            Assert.That(categories.First().Name, Is.EqualTo(category.Name));
        }

        [Test]
        public void ShouldLoadASubsetOfCategories()
        {
            // Arrange
            var ids = new[] { "id" };
            var category = new Category { Name = "Important" };
            var expectedCategories = new[] { category };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            
            // Indirect Input
            session.Load<Category>(ids).Returns(expectedCategories);
            
            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            IEnumerable<Category> categories = repository.GetAllCategories(ids);

            // Assert
            Assert.That(categories, Is.EquivalentTo(expectedCategories));
            Assert.That(categories.First().Name, Is.EqualTo(category.Name));
        }

        [Test]
        public void ShouldLoadAllCateogoriesIfNoIdsWereProvided()
        {
            // Arrange
            string[] ids = null;
            var category = new Category { Name = "Important" };
            var expectedCategories = new List<Category> { category };

            // Depedent-On Component
            var session = Substitute.For<IDocumentSession>();
            var query = Substitute.For<IRavenQueryable<Category>>();

            // Indirect Input
            session.Query<Category>().Returns(query);
            query.GetEnumerator().Returns(expectedCategories.GetEnumerator());

            // System under Test
            var repository = new RavenDbRepository(session);

            // Act
            IEnumerable<Category> categories = repository.GetAllCategories(ids);

            // Assert
            Assert.That(categories, Is.EquivalentTo(expectedCategories));
            Assert.That(categories.First().Name, Is.EqualTo(category.Name));
        }
    }
}