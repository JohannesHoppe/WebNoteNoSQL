namespace WebNoteAOP.Models.WebNoteCategory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Sample Repository: Subscriber
    /// </summary>
    public partial class WebNoteCategoryRepository : IWebNoteCategoryRepository
    {
        /// <summary>
        /// Gets the all categories for one note.
        /// </summary>
        public IEnumerable<Category> GetCategories(int noteId)
        {
            return (from c in this.Context.CategoryToNodes
                    where c.NoteId == noteId
                    select c.Category).ToList();
        }

        /// <summary>
        /// Gets all available categories.
        /// </summary>
        public IEnumerable<Category> GetAllCategories()
        {
            return (from c in this.Context.Categories select c).ToList();
        }

        /// <summary>
        /// First: Deletes all old relations between one note and its categories
        /// Second: adds the new relations
        /// </summary>
        public void UpdateRelation(int noteId, int[] newCategories)
        {
            this.DeleteAllRelations(noteId);

            if (newCategories != null)
            {
                foreach (int categoryId in newCategories)
                {
                    this.AddRelation(noteId, categoryId);
                }
            }
        }

        /// <summary>
        /// Deletes all old relations between one note and its categories
        /// </summary>
        public void DeleteAllRelations(int noteId)
        {
            var categoryToNodes = from c in this.Context.CategoryToNodes
                                  where c.NoteId == noteId
                                  select c;

            if (!categoryToNodes.Any())
            {
                return;
            }

            foreach (var categoryToNode in categoryToNodes)
            {
                this.Context.CategoryToNodes.DeleteObject(categoryToNode);
            }

            this.Context.SaveChanges();
        }


        /// <summary>
        /// Adds one relation between one note and one category
        /// </summary>
        /// <exception cref="ArgumentException" />
        private void AddRelation(int noteId, int categoryId)
        {
            Category checkForExistance = (from c in this.Context.Categories
                                          where c.CategoryId == categoryId
                                          select c).SingleOrDefault();

            if (checkForExistance == null)
            {
                throw new ArgumentException("A category with the given ID " + categoryId + " was not found!");
            }

            this.Context.CategoryToNodes.AddObject(
                new CategoryToNode
                    {
                        CategoryId = categoryId,
                        NoteId = noteId
                    });

            this.Context.SaveChanges();
        }
    }
}