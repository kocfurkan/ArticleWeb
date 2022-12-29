using Article_BLL;
using Article_Entities;
using Article_Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Article_Web.Controllers
{
	public class CommentController : Controller
	{
		CommentBL commentBL = new CommentBL();
		NoteBL noteBL = new NoteBL();
		// GET: Comment
		public ActionResult ShowComment(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
			}
			NoteBL noteBl = new NoteBL();
			Note note = noteBl.GetNoteById(id.Value);

			return PartialView("_PartialPageComments", note.Comments);
		}
		[AuthFilter]
		[HttpPost]
		public ActionResult Edit(int? id, string text)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				Comment comment = commentBL.GetCommentById(id.Value);
				if (comment == null)
				{
					return new HttpNotFoundResult();
				}
				comment.Text = text;
				if (commentBL.UpdateComment(comment) > 0)
				{
					return Json(new { result = true }, JsonRequestBehavior.AllowGet);
				}
				return Json(new { result = false }, JsonRequestBehavior.AllowGet);
			}
		}
		[AuthFilter]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Comment comment = commentBL.GetCommentById(id.Value);
			if (comment == null)
			{
				return new HttpNotFoundResult();
			}
			if (commentBL.DeleteComment(comment) > 0)
			{
				return Json(new { result = true }, JsonRequestBehavior.AllowGet);
			}
			return Json(new { result = false }, JsonRequestBehavior.AllowGet);
		}
		[AuthFilter]
		public ActionResult Create(Comment comment, int? noteId)
		{
			ModelState.Remove("UpdatedBy");
			if (ModelState.IsValid)
			{
				if (noteId == null)
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}

				Note note = noteBL.GetNoteById(noteId.Value);
				if (note == null)
				{
					return new HttpNotFoundResult();
				}
				comment.Note = note;
				comment.User = (User)Session["login"];

				int result = commentBL.AddComment(comment);
				if (result > 0)
				{
					return Json(new { result = true }, JsonRequestBehavior.AllowGet);
				}
			}
			return Json(new { result = false }, JsonRequestBehavior.AllowGet);
		}
	}
}