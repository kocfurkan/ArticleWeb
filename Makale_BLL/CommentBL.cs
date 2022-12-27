using Article_DAL;
using Article_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_BLL
{
    public class CommentBL
    {
        Repository<Comment> commentRepo = new Repository<Comment>();
        ResponsesBL<Comment> response = new ResponsesBL<Comment>();
        public Comment GetCommentById(int id)
        {
            return commentRepo.Find(x => x.Id == id);
        }

        public int UpdateComment(Comment comment)
        {
            return commentRepo.Update(comment);
        }

        public int DeleteComment(Comment comment)
        {
            return commentRepo.Delete(comment);
        }

        public int AddComment(Comment comment)
        {
            return commentRepo.Create(comment);
        }
    }
}
