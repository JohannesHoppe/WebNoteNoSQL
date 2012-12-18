namespace WebNoteAOP.Models.WebNote
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Adds data annotions to the generated code
    /// </summary>
    [MetadataType(typeof(NoteMetadata))]
    public partial class Note
    {
        public Note()
        {
            TypeDescriptor.AddProviderTransparent(
                new AssociatedMetadataTypeTypeDescriptionProvider(
                    typeof(Note),
                    typeof(NoteMetadata)),
                    typeof(Note));
        }
    }
}