using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assert = NUnit.Framework.Assert;

namespace WebNoteAOP.Tests.ValidationImplementation
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void CanValidate()
        {
            // Arrange
            YummySausageSalad yummySausageSalad = new YummySausageSalad { Name = "Wurstsalat" };
            Validator<YummySausageSalad> validator = new Validator<YummySausageSalad>();

            // Act
            bool actual = validator.IsValid(yummySausageSalad);

            // Assert
            Assert.True(actual);
        }

        [TestMethod]
        public void CanValidateAndDectectWrongData()
        {
            // Arrange
            YummySausageSalad yummySausageSalad = new YummySausageSalad { Name = "123456", Optional = "Test" };
            Validator<YummySausageSalad> validator = new Validator<YummySausageSalad>();

            // Act
            bool actual = validator.IsValid(yummySausageSalad);

            // Assert
            Assert.False(actual);
        }

        [TestMethod]
        public void CanReturnIModelStateDictionary()
        {
            // Arrange
            YummySausageSalad yummySausageSalad = new YummySausageSalad { Name = "123456" };
            Validator<YummySausageSalad> validator = new Validator<YummySausageSalad>();

            // Act
            IValidationResults actual = validator.Validate(yummySausageSalad);

            // Assert
            Assert.IsTrue(actual.ContainsKey<YummySausageSalad>(w => w.Name));
        }
    }
}
