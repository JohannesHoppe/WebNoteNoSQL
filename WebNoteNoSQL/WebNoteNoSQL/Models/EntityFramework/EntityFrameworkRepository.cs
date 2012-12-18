using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using Model = WebNoteNoSQL.Models;

namespace WebNoteNoSQL.Models.EntityFramework
{
    public class EntityFrameworkRepository : IWebNoteRepository, IDisposable
    {
        private readonly WebNote context;

        public EntityFrameworkRepository()
        {
            context = new WebNote();
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            return (from n in context.Notes
                    orderby n.NoteId descending
                    select
                        new NoteWithCategories
                            {
                                Id = SqlFunctions.StringConvert((double)n.NoteId).Trim(),
                                Title = n.Title,
                                Message = n.Message,
                                Added = n.Added,
                                Categories = from c in n.Categories
                                             select
                                                 new Model.Category
                                                     {
                                                         Id = SqlFunctions.StringConvert((double)c.CategoryId).Trim(),
                                                         Name = c.Name,
                                                         Color = c.Color
                                                     }
                            }).ToList();
        }

        public NoteWithCategories Read(string id)
        {
            int intId = id.AsInt32();
            return (from n in context.Notes
                    where n.NoteId == intId
                    select
                        new NoteWithCategories
                            {
                                Id = SqlFunctions.StringConvert((double)n.NoteId).Trim(),
                                Title = n.Title,
                                Message = n.Message,
                                Added = n.Added,
                                Categories = from c in n.Categories
                                             select
                                                 new Model.Category
                                                     {
                                                         Id = SqlFunctions.StringConvert((double)c.CategoryId).Trim(),
                                                         Name = c.Name,
                                                         Color = c.Color
                                                     }
                            }).FirstOrDefault();
        }

        public void Create(Models.Note noteToAdd, IEnumerable<Models.Category> newCategories)
        {
            Note newNote = new Note { Title = noteToAdd.Title, Message = noteToAdd.Message, Added = DateTime.Now };

            context.Notes.AddObject(newNote);
            context.SaveChanges();

            UpdateRelation(newNote, newCategories);
        }

        public void Update(Models.Note noteToEdit, IEnumerable<Models.Category> newCategories)
        {
            Note note = EditNote(noteToEdit);
            UpdateRelation(note, newCategories);
        }

        public void Delete(string id)
        {
            int intId = id.AsInt32();
            Note noteToDelete = this.context.Notes.FirstOrDefault(n => n.NoteId == intId);

            if (noteToDelete == null)
            {
                return;
            }

            UpdateRelation(noteToDelete, new List<Models.Category>());

            context.Notes.DeleteObject(noteToDelete);
            context.SaveChanges();
        }

        public IEnumerable<Models.Category> GetAllCategories()
        {
            return (from c in context.Categories
                    orderby c.CategoryId ascending
                    select
                        new Models.Category
                            {
                                Id = SqlFunctions.StringConvert((double)c.CategoryId).Trim(),
                                Name = c.Name,
                                Color = c.Color
                            }).ToList();
        }

        public IEnumerable<Models.Category> GetAllCategories(string[] categoryIds)
        {
            if (categoryIds == null)
            {
                return new List<Models.Category>();
            }

            IEnumerable<int> categoriesIdsAsInt = categoryIds.Select(c => c.AsInt32());

            return (from c in context.Categories
                    where categoriesIdsAsInt.Any(x => x == c.CategoryId)
                    orderby c.CategoryId ascending
                    select
                        new Models.Category
                            {
                                Id = SqlFunctions.StringConvert((double)c.CategoryId).Trim(),
                                Name = c.Name,
                                Color = c.Color
                            }).ToList();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public void TryInstallDemoContent()
        {
            // not required, delivered database is already set up
        }

        private Note EditNote(Models.Note noteData)
        {
            int intId = noteData.Id.AsInt32();
            Note noteToEdit = this.context.Notes.FirstOrDefault(n => n.NoteId == intId);

            if (noteToEdit == null)
            {
                return null;
            }

            noteToEdit.Title = noteData.Title;
            noteToEdit.Message = noteData.Message;
            context.SaveChanges();

            return noteToEdit;
        }

        private void UpdateRelation(Note updatedNote, IEnumerable<Models.Category> newCategories)
        {
            if (updatedNote == null)
            {
                return;
            }

            RemoveAllOldCategories(updatedNote);
            AttachNewCategories(updatedNote, newCategories);
        }

        private void RemoveAllOldCategories(Note updatedNote)
        {
            List<Category> categoriesToRemove = updatedNote.Categories.ToList();
            categoriesToRemove.ForEach(c => updatedNote.Categories.Remove(c));
            context.SaveChanges();
        }

        private void AttachNewCategories(Note updatedNote, IEnumerable<Models.Category> newCategories)
        {
            if (newCategories == null || !newCategories.Any())
            {
                return;
            }

            List<int> categoryIds = newCategories.Select(ids => ids.Id.AsInt32()).ToList();

            List<Category> categoriesToAttach =
                (from c in context.Categories where categoryIds.Contains(c.CategoryId) select c).ToList();

            categoriesToAttach.ForEach(c => updatedNote.Categories.Add(c));
            context.SaveChanges();
        }
    }
}