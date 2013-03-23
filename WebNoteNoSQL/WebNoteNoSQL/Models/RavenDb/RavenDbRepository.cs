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

            throw new NotImplementedException();
        }

        public NoteWithCategories Read(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Note noteToEdit, IEnumerable<Category> newCategories)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories(string[] categoryIds)
        {
            throw new NotImplementedException();
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