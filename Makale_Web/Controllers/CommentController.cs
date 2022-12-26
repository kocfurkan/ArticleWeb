using Article_BLL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Article_Web.Controllers
{
    public class CommentController : Controller
    {
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
    }
}