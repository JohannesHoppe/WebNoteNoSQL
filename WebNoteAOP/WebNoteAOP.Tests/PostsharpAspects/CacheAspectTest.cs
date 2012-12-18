namespace WebNoteAOP.Tests.PostsharpAspects
{
    using System.Collections.Generic;

    using global::PostsharpAspects.Caching.CacheImplementation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NUnit.Framework;

    using WebNoteAOP.Models;
    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// Test for the aspect Cache
    /// </summary>
    [TestClass]
    public class CacheAspectTest
    {
        private WebNoteService webNoteService;
        private Mock<IWebNoteRepository> repository;
        private Mock<IWebNoteCategoryRepository> categoryRepository;

        private IUnitTestableCache cache;

        [TestInitialize]
        public void InitializeService()
        {
            #region mock repository & categoryRepository
            
            Note expectedNote = new Note { NoteId = 1, Title = "Test" };
            IEnumerable<Category> expectedListOfCategories = new List<Category>();

            this.repository = new Mock<IWebNoteRepository>();
            this.repository.Setup(r => r.GetNote(expectedNote.NoteId)).Returns(expectedNote);

            this.categoryRepository = new Mock<IWebNoteCategoryRepository>();
            this.categoryRepository.Setup(s => s.GetCategories(expectedNote.NoteId)).Returns(expectedListOfCategories);

            #endregion

            this.webNoteService = new WebNoteService
                {
                    WebNoteRepository = this.repository.Object, 
                    WebNoteCategoryRepository = this.categoryRepository.Object
                };

            this.cache = new Cache("x");
            this.cache.CleanCompleteCache();
        }

        [TestMethod]
        public void CanCacheTest()
        {
            // Act
            var actual1 = this.webNoteService.Read(1);

            // Assert
            Assert.That(actual1.NoteId, Is.EqualTo(1));
            this.repository.Verify(r => r.GetNote(1), Times.Exactly(1));

            // Act - now cache should prevent a second call
            var actual2 = this.webNoteService.Read(1);

            // Assert
            Assert.That(actual2.NoteId, Is.EqualTo(1));
            this.repository.Verify(r => r.GetNote(1), Times.Exactly(1), "+++ Do you really added the aspect to the service? +++");
        }

        [TestMethod]
        public void UpdateWillRemoveItemFromCache()
        {
            // Arrange
            Note noteToChance = new Note { NoteId = 1, Title = "New", Message = "New" };
            
            // Act - this item will be cached
            this.webNoteService.Read(1);
            var cacheActual = this.cache.GetFirstItemFromCache();

            // Assert - the item should be in the cache now
            Assert.That(cacheActual, Is.Not.Null, "+++ Do you really added the aspect to the service? +++");

            /* --- */

            // Act - Update should force a remove
            this.webNoteService.Update(noteToChance, null);
            var cacheActual2 = this.cache.GetFirstItemFromCache();

            // Assert - is null when item was removed
            Assert.IsNull(cacheActual2);
        }
    }
}
