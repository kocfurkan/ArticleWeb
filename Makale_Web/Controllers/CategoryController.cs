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


namespace Makale_Web.Controllers
{
	public class CategoryController : Controller
	{
		CategoryBL db = new CategoryBL();

		// GET: Category
		public ActionResult Index()
		{
			return View(db.ReadCategories());
		}

		// GET: Category/Details/5
		public ActionResult Details(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// GET: Category/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Category/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category category)
		{
			if (ModelState.IsValid)
			{
				db.AddCategory(category);
				return RedirectToAction("Index");
			}

			return View(category);
		}

		// GET: Category/Edit/5
		public ActionResult Edit(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// POST: Category/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				db.UpdateCategory(category);
				return RedirectToAction("Index");
			}
			return View(category);
		}

		// GET: Category/Delete/5
		public ActionResult Delete(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// POST: Category/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Category category = db.GetCategory(id);
			db.DeleteCategory(category);
			return RedirectToAction("Index");
		}


	}
}
