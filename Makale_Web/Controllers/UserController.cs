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
using Makale_Entities.ViewModels;


namespace Makale_Web.Controllers
{
	public class UserController : Controller
	{
		UserBL db = new UserBL();

		// GET: User
		public ActionResult Index()
		{
			return View(db.GetUsers());
		}

		// GET: User/Details/5
		public ActionResult Details(int id)
		{

			User user = db.GetUserById(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(SignupModel registeringusr)
		{
			if (ModelState.IsValid)
			{
				db.SignUp(registeringusr);

				return RedirectToAction("SignupSuccess");
			}
			return View(registeringusr);

		}

		// GET: User/Edit/5
		public ActionResult Edit(int id)
		{

			User user = db.GetUserById(id);

			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				db.UpdateUser(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: User/Delete/5
		public ActionResult Delete(int id)
		{

			User user = db.GetUserById(id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: User/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			User user = db.GetUserById(id);
			db.DeleteUser(user);
			return RedirectToAction("Index");
		}


	}
}
