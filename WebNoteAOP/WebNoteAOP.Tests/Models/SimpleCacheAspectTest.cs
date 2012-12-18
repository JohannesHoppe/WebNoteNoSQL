namespace WebNoteAOP.Tests.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using NUnit.Framework;

    using WebNoteAOP.Models;
    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// Test for the aspect SimpleCache
    /// </summary>
    [TestClass]
    public class SimpleCacheAspectTest
    {
        private WebNoteService sut;
        private Mock<IWebNoteRepository> repository;
        private Mock<IWebNoteCategoryRepository> categoryRepository;

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

            this.sut = new WebNoteService
                {
                    WebNoteRepository = this.repository.Object, 
                    WebNoteCategoryRepository = this.categoryRepository.Object
                };

            #region clean HttpRuntime.Cache 

            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
            #endregion
        }


        [TestMethod]
        public void CanCacheTest()
        {
            // Act
            var actual1 = this.sut.Read(1);

            // Assert
            Assert.That(actual1.NoteId, Is.EqualTo(1));
            this.repository.Verify(r => r.GetNote(1), Times.Exactly(1));

            // Act - now cache should prevent a second call
            var actual2 = this.sut.Read(1);

            // Assert
            Assert.That(actual2.NoteId, Is.EqualTo(1));
            this.repository.Verify(r => r.GetNote(1), Times.Exactly(1));
        }

        [TestMethod]
        public void UpdateWillRemoveItemFromCache()
        {
            // Arrange
            const string Prefix = "WebNoteAOP.Models.WebNoteService_";
            Note noteToChance = new Note { NoteId = 1, Title = "Neu" };
            
            // Act - this item will be cached
            this.sut.Read(1);
            var cacheActual = HttpRuntime.Cache[Prefix + 1];

            // Assert - the item should be in the cache now
            Assert.That(cacheActual, Is.Not.Null);

            /* --- */

            // Act - Update should force a remove
            this.sut.Update(noteToChance, null);
            var cacheActual2 = HttpRuntime.Cache[Prefix + 1];

            // Assert - is null when item was removed
            Assert.IsNull(cacheActual2);
        }
    }
}
