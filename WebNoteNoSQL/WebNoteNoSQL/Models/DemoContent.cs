using System;
using System.Collections.Generic;

namespace WebNoteNoSQL.Models
{
    public static class DemoContent
    {
        public static List<NoteWithCategories> GetNodesWithCategories(string engine, Func<string> getNextId)
        {
            return new List<NoteWithCategories>
                {
                    new NoteWithCategories
                        {
                            Id = getNextId(),
                            Title = engine + " Testeintrag",
                            Message = "Ein gruener Postit",
                            Added = new DateTime(2012, 05, 13),
                            Categories =
                                new List<Category>
                                    {
                                        new Category { Name = "Normal Importance", Color = "green" },
                                        new Category { Name = "Private", Color = "gray" },
                                    }
                        },
                    new NoteWithCategories
                        {
                            Id = getNextId(),
                            Title = engine + " Testeintrag 2",
                            Message = "Ein roter Postit",
                            Added = new DateTime(2012, 05, 14),
                            Categories =
                                new List<Category> { new Category { Name = "High Importance", Color = "red" } }
                        },
                    new NoteWithCategories
                        {
                            Id = getNextId(),                            
                            Title = engine + " Testeintrag 3",
                            Message = "Ein privater Postit",
                            Added = new DateTime(2012, 05, 14),
                            Categories = new List<Category> { new Category { Name = "Private", Color = "gray" } }
                        }
                };            
        }

        public static List<Category> GetCategories(Func<string> getNextId)
        {
            return new List<Category>
                {
                    new Category
                        {
                            Id = getNextId(),                            
                            Name = "Normal Importance",
                            Color = "green"
                        },
                    new Category
                        {
                            Id = getNextId(),
                            Name = "High Importance",
                            Color = "red"
                        },
                    new Category
                        {
                            Id = getNextId(),
                            Name = "Private",
                            Color = "gray"
                        }
                };
        }
    }
}