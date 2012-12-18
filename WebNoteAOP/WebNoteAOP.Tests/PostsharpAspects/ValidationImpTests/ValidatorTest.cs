namespace WebNoteAOP.Tests.PostsharpAspects.ValidationImpTests
{
    using global::PostsharpAspects.Validation.ValidationImplementation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Assert = NUnit.Framework.Assert;

    [TestClass]
    public class ValidatorTest
    {
        private readonly IValidator validator = new DataAnnotationsValidator();

        [TestMethod]
        public void CanValidateCorrectData()
        {
            // Arrange
            ValidatorTestObject validatorTestObject = new ValidatorTestObject { Name = "Test" };

            // Act
            bool actual = this.validator.IsValid(validatorTestObject);

            // Assert
            Assert.True(actual);
        }

        [TestMethod]
        public void CanValidateWrongData()
        {
            // Arrange
            ValidatorTestObject validatorTestObject = new ValidatorTestObject { };

            // Act
            bool actual = this.validator.IsValid(validatorTestObject);

            // Assert
            Assert.False(actual);
        }

        [TestMethod]
        public void CanReturnIModelStateDictionary()
        {
            // Arrange
            ValidatorTestObject validatorTestObject = new ValidatorTestObject { Name = "123456" };

            // Act
            IValidationResults actual = this.validator.Validate(validatorTestObject);

            // Assert
            Assert.IsTrue(actual.ContainsKey<ValidatorTestObject>(w => w.Name));
        }
    }
}
