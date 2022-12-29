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
using Article_Web.Filters;

namespace Article_Web.Controllers
{
	[AuthFilter]
	[AdminFilter]
	public class CategoryController : Controller
	{
		CategoryBL db = new CategoryBL();


		public ActionResult Index()
		{
			return View(db.ReadCategories());
		}

		
		public ActionResult Details(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		[AuthFilter]
		public ActionResult Create()
		{
			return View();
		}

	
		[AuthFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Category category)
		{
			//Removes validation for given property of entity.

			ModelState.Remove("UpdatedBy");
			if (ModelState.IsValid)
			{
				ResponsesBL<Category> response = db.AddCategory(category);
				if (response.errors.Count > 0)
				{
					response.errors.ForEach(x => ModelState.AddModelError("", x));
					return View(category);
				}
				return RedirectToAction("Index");
			}
			return View(category);
		}

		[AuthFilter]
	
		public ActionResult Edit(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		[AuthFilter]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category category)
		{

			ModelState.Remove("UpdatedBy");
			if (ModelState.IsValid)
			{
				ResponsesBL<Category> response = db.UpdateCategory(category);
				if (response.errors.Count > 0)
				{
					response.errors.ForEach(x => ModelState.AddModelError("", x));
					return View(category);
				}
				return RedirectToAction("Index");
			}
			return View(category);
		}

		[AuthFilter]
		public ActionResult Delete(int id)
		{
			Category category = db.GetCategory(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		[AuthFilter]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			ResponsesBL<Category> response = new ResponsesBL<Category>();
			response.Obj = db.GetCategory(id);
			db.DeleteCategory(response.Obj);
			return RedirectToAction("Index");
		}


	}
}
