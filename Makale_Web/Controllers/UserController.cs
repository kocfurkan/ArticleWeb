using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Article_BLL;
using Article_Entities;
using Article_Entities.ViewModels;
using Article_Web.Filters;

namespace Article_Web.Controllers
{
	[AdminFilter]
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
		public ActionResult Create(User user)
		{
			ModelState.Remove("UpdatedBy");
			if (ModelState.IsValid)
			{
				db.CreateUserByAdmin(user);

				return RedirectToAction("Index");
			}
			return View(user);

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
			ModelState.Remove("UpdatedBy");
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
