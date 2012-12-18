namespace WebNoteAOP.Models.WebNote
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Sets Title and Message required
    /// </summary>
    public class NoteMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a title!")]
        public virtual string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide a message!")]
        public virtual string Message { get; set; }
    }
}