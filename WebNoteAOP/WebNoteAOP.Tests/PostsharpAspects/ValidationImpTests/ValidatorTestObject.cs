namespace WebNoteAOP.Tests.PostsharpAspects.ValidationImpTests
{
    using System.ComponentModel.DataAnnotations;

    public class ValidatorTestObject
    {
        [Required]
        [StringLength(10)]
        [RegularExpression("[A-Za-z\\s]+")]
        public string Name
        {
            get;
            set;
        }

        public string Optional
        {
            get;
            set;
        }
    }
}
