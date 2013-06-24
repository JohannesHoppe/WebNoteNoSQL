using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.Unity;
using Raven.Client;
using Raven.Client.Document;

namespace WebNoteNoSQL.Models.RavenDb
{
    using System;

    public class RavenDbRepository : IWebNoteRepository
    {
        private const string ConnectionStringName = "RavenDB";

        private readonly IDocumentSession session;
        private readonly IDocumentStore store;

        [InjectionConstructor]
        public RavenDbRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            store = new DocumentStore
                {
                    Url = connectionString,
                    Conventions = new DocumentConvention { IdentityPartsSeparator = "-" } // This is required because otherwise the default '/' is used, which creates a collision with MVC url mappings
                }.Initialize();
            session = this.store.OpenSession();
        }

        // ctor for Unit Tests
        public RavenDbRepository(IDocumentSession session)
        {
            this.session = session;
        }

        public void Create(Note noteToAdd, IEnumerable<Category> newCategories)
        {
            NoteWithCategories note = NoteWithCategories.Convert(noteToAdd, newCategories);
            session.Store(note);
            session.SaveChanges();
        }

        public NoteWithCategories Read(string id)
        {
            return session.Load<NoteWithCategories>(id);
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            return session.Query<NoteWithCategories>().ToList();
        }

        public void Update(Note noteToEdit, IEnumerable<Category> newCategories)
        {
            Create(noteToEdit, newCategories);
        }

        public void Delete(string id)
        {
            var note = session.Load<NoteWithCategories>(id);
            session.Delete(note);
            session.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return session.Query<Category>().ToList();
        }

        public IEnumerable<Category> GetAllCategories(string[] categoryIds)
        {
            return categoryIds == null ? GetAllCategories() : session.Load<Category>(categoryIds).AsEnumerable();
        }

        public void Dispose()
        {
            if (session != null)
            {
                session.Dispose();
            }            
        }

        #region Install Demo Content

        public void TryInstallDemoContent()
        {
            if (session.Query<NoteWithCategories>().Any())
            {
                return;
            }

            var newNotes = DemoContent.GetNodesWithCategories("Redis", () => null);
            foreach (var newNote in newNotes)
            {
                session.Store(newNote);
            }

            session.SaveChanges();

            var newCategories = DemoContent.GetCategories(() => null);
            foreach (var newCategory in newCategories)
            {
                session.Store(newCategory);
            }

            session.SaveChanges();
        }

        #endregion
    }
}