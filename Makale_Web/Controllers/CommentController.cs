using Article_BLL;
using Article_Entities;
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
        public ActionResult Create(Comment comment, int? noteid)
        {
            ModelState.Remove("UpdatedBy");
            if (ModelState.IsValid)
            {
                if (noteid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Note note = noteBL.GetNoteById(noteid.Value);
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