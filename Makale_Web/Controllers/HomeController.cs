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
                RedirectToAction("Index");
            }
            return View(usr);
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
                return RedirectToAction("Login");
            }
            return View(registeringusr);
        }

        public ActionResult ActivateUser(Guid ActivationGuid)
        {
            return View();
        }
    }
}