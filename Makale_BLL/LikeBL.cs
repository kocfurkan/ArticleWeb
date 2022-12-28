using Article_DAL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public List<Like> ListLikes(Expression<Func<Like, bool>> condition)
		{
			return repoLikes.Read(condition);
		}
		public Like FindLike(int noteId, int userId)
		{
			return repoLikes.Find(x=>x.Note.Id == noteId && x.User.Id == userId);
		}
		public int AddLike(Like Like)
		{
			return repoLikes.Create(Like);
		}
		public int RemoveLike(Like like)
		{
			return repoLikes.Delete(like);
		}
	} 
}
