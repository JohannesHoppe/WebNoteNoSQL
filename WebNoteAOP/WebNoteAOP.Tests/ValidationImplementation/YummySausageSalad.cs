using System.ComponentModel.DataAnnotations;

namespace WebNoteAOP.Tests.ValidationImplementation
{
    public class YummySausageSalad
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
