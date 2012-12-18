using System.Collections.Generic;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using WebNoteNoSQL.Code;
using WebNoteNoSQL.Models;

namespace WebNoteNoSQL.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Configuration;

    [FillViewData]
    public class HomeController : Controller
    {
        [Dependency]
        public IWebNoteRepository Repository { private get; set; }

        public ActionResult Index()
        {
            IEnumerable<NoteWithCategories> notes = Repository.ReadAll();
            return View(notes);
        }

        public ActionResult Details(string id)
        {
            NoteWithCategories note = Repository.Read(id);
            return View(note);
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Note noteToAdd, string[] newCategories)
        {
            if (ModelState.IsValid)
            {
                var categories = Repository.GetAllCategories(newCategories);
                Repository.Create(noteToAdd, categories);
                return RedirectToAction("Index");
            }

            return View(noteToAdd);
        }

        public ActionResult Edit(string id)
        {
            NoteWithCategories note = Repository.Read(id);

            return View(note);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Note noteToEdit, string[] newCategories)
        {
            var categories = Repository.GetAllCategories(newCategories);
            Repository.Update(noteToEdit, categories);

            return RedirectToAction("Details", new { id = noteToEdit.Id });
        }

        public ActionResult Delete(string id)
        {
            Repository.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult ChangeEngine()
        {
            WebConfigManipulation.ChangeEngine();

            return RedirectToAction("Index");
        }
    }
}