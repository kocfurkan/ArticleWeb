using Article_DAL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_BLL
{
	public class CategoryBL
	{
		Repository<Category> repoCategory = new Repository<Category>();
		Repository<Note> repoNote = new Repository<Note>();
		Repository<Like> repoLike = new Repository<Like>();
		Repository<Comment> repoComment = new Repository<Comment>();


		public List<Category> ReadCategories()
		{
			return repoCategory.Read();
		}

		public Category GetCategory(int id)
		{
			return repoCategory.Find(x => x.Id == id);
		}

		public ResponsesBL<Category> AddCategory(Category category)
		{
			ResponsesBL<Category> response = new ResponsesBL<Category>();
			response.Obj = repoCategory.Find(x => x.Title == category.Title);
			if (response.Obj != null)
			{
				response.errors.Add("Category Entered Already Exist");
			}
			else
			{
				int success = repoCategory.Create(category);
				if (success < 1)
				{
					response.errors.Add("Failed to Create Category");
				}
			}
			return response;
		}

		public ResponsesBL<Category> UpdateCategory(Category category)
		{
			
			ResponsesBL<Category> response = new ResponsesBL<Category>();
			response.Obj = repoCategory.Find(x => x.Id == category.Id);
			if (response.Obj != null)
			{
				response.Obj.Title = category.Title;
				response.Obj.Description = category.Description;
				int success = repoCategory.Update(response.Obj);
				if (success < 1)
				{
					response.errors.Add("Failed to Update the Category, Please Check Your Inputs");
				}
			}
			return response;
		}

		public ResponsesBL<Category> DeleteCategory(Category category)
		{
			ResponsesBL<Category> response = new ResponsesBL<Category>();
			response.Obj = repoCategory.Find(x => x.Title==category.Title);
			if (response.Obj != null)
			{
				//Category requires its notes comments and likes to be deleted when itself is deleted
				if (response.Obj != null)
				{
					foreach (Note note in response.Obj.Notes.ToList())
					{
						foreach (Comment comment in note.Comments.ToList())
						{
							repoComment.Delete(comment);
						}
						foreach (Like likes in note.Likes.ToList())
						{
							repoLike.Delete(likes);
						}
						repoNote.Delete(note);
					}
				}
				repoCategory.Delete(response.Obj);
			}
			else
			{
				response.errors.Add("User Cannot Be Found");
			}
			return response;
		}
	}
}
