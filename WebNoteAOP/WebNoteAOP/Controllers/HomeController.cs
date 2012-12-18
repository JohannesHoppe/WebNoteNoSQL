using System;
using System.Collections.Generic;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

using PostsharpAspects.ExceptionHandling;
using PostsharpAspects.Validation.ValidationImplementation;

using WebNoteAOP.Code;
using WebNoteAOP.Models;
using WebNoteAOP.Models.WebNote;

namespace WebNoteAOP.Controllers
{
    [FillViewData]
    public class HomeController : Controller
    {
        /// <summary>
        /// Gets or sets the required WebNoteService
        /// </summary>
        [Dependency]
        public IWebNoteService WebNoteService { private get; set; }

        //// doing a simple cast
        private IValidator ServiceAsValidator
        {
            get
            {
                return (IValidator)this.WebNoteService;
            }
        }

        /// <summary>
        /// Returns the index view
        /// </summary>
        public ActionResult Index()
        {
            IEnumerable<NoteWithCategories> notes = this.WebNoteService.ReadAll();

            return View(notes);
        }

        /// <summary>
        /// Returns a view with details about one note
        /// </summary>
        public ActionResult Details(int id)
        {
            NoteWithCategories note = this.WebNoteService.Read(id);

            return View(note);
        }

        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult Create(Note noteToAdd, int[] newCategories)
        {
            #region using the validator
            //// UNDONE: ValidatorIntroduceAspect - using the interface
            #if false
            if (!this.ServiceAsValidator.IsValid(noteToAdd))
            {
                IValidationResults result = this.ServiceAsValidator.Validate(noteToAdd);
                this.ReplaceModelState(result);
                return View();
            }

            #endif
            #endregion

            this.WebNoteService.Create(noteToAdd, newCategories);

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            NoteWithCategories note = this.WebNoteService.Read(id);
            return View(note);
        }

        // UNDONE: ConvertExceptionAspect --> this will thow an exception on an empty title, but we need a MyException
        [HandleError(ExceptionType = typeof(MyException))]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Note noteToEdit, int[] newCategories)
        {
            #region using the validator
            //// UNDONE: ValidatorIntroduceAspect - using the interface
            #if false

            if (!this.ServiceAsValidator.IsValid(noteToEdit))
            {
                IValidationResults result = this.ServiceAsValidator.Validate(noteToEdit);
                this.ReplaceModelState(result);
                return this.Edit(noteToEdit.NoteId);
            }

            #endif
            #endregion

            NoteWithCategories changedNote = this.WebNoteService.Update(noteToEdit, newCategories);
            return View(changedNote);
        }

        public ActionResult Delete(int id)
        {
            this.WebNoteService.Delete(id);
            return RedirectToAction("Index");
        }

        #region small helpers

        private void ReplaceModelState(IValidationResults results)
        {
            ModelState.Clear();
            ModelState.Merge(results.AsModelStateDictionary);
        }

        #endregion
    }
}