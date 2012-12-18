namespace WebNoteAOP.Tests.PostsharpAspects
{
    using global::PostsharpAspects.Validation.ValidationImplementation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    using NUnit.Framework;

    using WebNoteAOP.Models;
    using WebNoteAOP.Models.WebNote;
    using WebNoteAOP.Models.WebNoteCategory;

    using Assert = NUnit.Framework.Assert;

    /// <summary>
    /// Test for the aspect DateTimeNowOverride
    /// </summary>
    [TestClass]
    public class ValidationGuardAspectTest 
    {
        private WebNoteService webNoteService;
        private Mock<IWebNoteRepository> repository;
        private Mock<IWebNoteCategoryRepository> categoryRepository;

        [TestInitialize]
        public void InitializeService()
        {
            this.repository = new Mock<IWebNoteRepository>();
            this.categoryRepository = new Mock<IWebNoteCategoryRepository>();

            this.webNoteService = new WebNoteService
            {
                WebNoteRepository = this.repository.Object,
                WebNoteCategoryRepository = this.categoryRepository.Object
            };
        }

        [TestMethod]
        public void CanAcceptCorrectData()
        {
            // Arrange
            Note wrongNote = new Note { Title = "Test", Message = "Test" };

            // Act
            TestDelegate createAction = () => this.webNoteService.Create(wrongNote, null);

            // Assert
            Assert.DoesNotThrow(createAction);
        }

        [TestMethod]
        public void CanThrowDataNotValidException()
        {
            // Arrange
            Note wrongNote = new Note();

            // Act
            TestDelegate createAction = () => this.webNoteService.Create(wrongNote, null);

            // Assert
            Assert.Throws<DataNotValidException>(createAction, "+++ Do you really added the aspect to the service? +++");
        }
    }
}
