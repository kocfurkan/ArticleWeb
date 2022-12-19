using Makale_BLL;
using Makale_Entities;
using Makale_Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Makale_Web.Controllers
{
    public class HomeController : Controller
    {
        NoteBL noteBl = new NoteBL();
        UserBL usrBl = new UserBL();
        // GET: Home
        public ActionResult Index()
        {
            //Test test = new Test();
            //test.InsertTest();
            //test.UpdateTest();
            //test.DeleteTest();
            //test.InsertComment();

            return View(noteBl.ReadNotes().OrderByDescending(x => x.UpdateDate).ToList());
        }

        public ActionResult Category(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            CategoryBL categoryBL = new CategoryBL();
            Category category = categoryBL.GetCategory(Id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View("Index", category.Notes);
        }

        public ActionResult MostLiked()
        {
            //A new model is sent to index on this action which has ordering of notes by LikeNumber
            return View("Index", noteBl.ReadNotes().OrderByDescending(x => x.LikeNumber).ToList());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel usr)
        {
            if (ModelState.IsValid)
            {
                ResponsesBL<User> response = usrBl.LogIn(usr);
                if (response.errors.Count > 0)
                {
                    response.errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(usr);
                }
                //A Session is created on login
                Session["login"] = response.Obj;
                return RedirectToAction("Index");
            }
            return View(usr);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(SignupModel registeringusr)
        {
            if (ModelState.IsValid)
            {
                ResponsesBL<User> response = usrBl.SignUp(registeringusr);
                if (response.errors.Count > 0)
                {
                    response.errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(registeringusr);
                }
                return RedirectToAction("SignupSuccess");
            }
            return View(registeringusr);
        }

        public ActionResult SignupSuccess()
        {
            return View();
        }

        public ActionResult ActivateUser(Guid ActivationGuid)
        {
            ResponsesBL<User> response = usrBl.ActivateUser(ActivationGuid);
            if (response.errors.Count > 0)
            {
                TempData["errors"] = response.errors;
                return RedirectToAction("ActivateUserFailed");
            }

            return View();
        }
        public ActionResult ActivateUserFailed()
        {
            List<string> errors = null;

            if (TempData["errors"] != null)
            {
                errors = (List<string>)TempData["errors"];
            }
            return View(errors);
        }

        public ActionResult ProfilePage()
        {
            User usr = (User)Session["login"];
            return View(usr);
        }
        public ActionResult ProfileEdit()
        {
            User usr = (User)Session["login"];
            return View(usr);
        }
        [HttpPost]
        public ActionResult ProfileEdit(User usr, HttpPostedFile Avatar)
        {
            if (Avatar != null && Avatar.ContentType == "image/jpg")
            {
                string fileName = $"user_{usr.Id}.{Avatar.ContentType.Split('/')[0]}";
                Avatar.SaveAs(Server.MapPath($"~/image/{fileName}"));
                usr.Avatar = fileName;
            }
            ResponsesBL<User> result = usrBl.UpdateUser(usr);
            if (result.errors.Count > 0)
            {
                result.errors.ForEach(x => ModelState.AddModelError("", x));
                return View(result.Obj);
            }
            return View(usr);
        }
        public ActionResult ProfileDelete()
        {
            return View();
        }

    }
}