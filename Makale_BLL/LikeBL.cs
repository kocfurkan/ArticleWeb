using Article_DAL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_BLL
{
	public class LikeBL
	{
		Repository<Like> repoLikes = new Repository<Like>();
		public IQueryable<Like> GetLikesQueryable()
		{
			return repoLikes.ReadQueryable();
		}
	} 
}
