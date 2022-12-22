using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Makale_BLL;
using Makale_Entities;
using Makale_Web.Data;

namespace Makale_Web.Controllers
{
	public class NoteController : Controller
	{
		NoteBL db = new NoteBL();
		CategoryBL categoryBl = new CategoryBL();
		LikeBL likeBL = new LikeBL();
		// GET: Note
		public ActionResult Index()
		{
			var notes = db.ReadNotesQueryable().Include(n => n.Category);
			if (Session["loign"] != null)
			{
				User usr = (User)Session["login"];
				notes = db.ReadNotesQueryable().Include(n => n.Category).Include(k => k.User).Where(x => x.User.Id == usr.Id);
			}

			return View(notes.ToList());
		}
		public ActionResult Likes()
		{

			var notes = db.ReadNotesQueryable().Include(n => n.Category);
			if (Session["loign"] != null)
			{
				User usr = (User)Session["login"];
				notes = likeBL.GetLikesQueryable().Include("User").Include("Note").Where(x => x.User.Id == usr.Id).Select(x=>x.Note).Include(k=>k.Category);
			}
			return View("Index", notes.ToList());
		}

		// GET: Note/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.GetNoteById(id);
			if (note == null)
			{
				return HttpNotFound();
			}
			return View(note);
		}

		// GET: Note/Create
		public ActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(categoryBl.ReadCategories(), "Id", "Title");
			return View();
		}

		// POST: Note/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Note note)
		{
			if (ModelState.IsValid)
			{
				db.SaveNote(note);
				return RedirectToAction("Index");
			}

			ViewBag.CategoryId = new SelectList(categoryBl.ReadCategories(), "Id", "Title", note.CategoryId);
			return View(note);
		}

		// GET: Note/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.GetNoteById(id);
			if (note == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategoryId = new SelectList(categoryBl.ReadCategories(), "Id", "Title", note.CategoryId);
			return View(note);
		}

		// POST: Note/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Note note)
		{
			if (ModelState.IsValid)
			{
				db.UpdateNote(note);

				return RedirectToAction("Index");
			}
			ViewBag.CategoryId = new SelectList(categoryBl.ReadCategories(), "Id", "Title", note.CategoryId);
			return View(note);
		}

		// GET: Note/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Note note = db.GetNoteById(id);
			if (note == null)
			{
				return HttpNotFound();
			}
			return View(note);
		}

		// POST: Note/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Note note = db.GetNoteById(id);
			db.DeleteNote(note);
			return RedirectToAction("Index");
		}


	}
}
