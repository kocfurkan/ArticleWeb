using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class CategoryBL
    {
        Repository<Category> repoCategory = new Repository<Category>();

        public List<Category> ReadCategories()
        {
            return repoCategory.Read();
        }

        public Category GetCategory(int id)
        {
            return repoCategory.Find(x=> x.Id == id);
        }

		public void AddCategory(Category category)
		{
			throw new NotImplementedException();
		}

		public void UpdateCategory(Category category)
		{
			throw new NotImplementedException();
		}

		public void DeleteCategory(Category category)
		{
			throw new NotImplementedException();
		}
	}
}
