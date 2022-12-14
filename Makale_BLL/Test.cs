using Makale_DAL;
using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_BLL
{
    public class Test
    {
        Repository<User> repoUsr = new Repository<User>();
        Repository<Comment> repoComment = new Repository<Comment>();
        Repository<Note> repoNote = new Repository<Note>();

        public Test()
        {
            //DatabaseContext db = new DatabaseContext();
            //db.Categories.ToList();

            Repository<Category> repoCtg = new Repository<Category>();
            List<Category> ctgList = repoCtg.Read();
            List<Category> ctgList2 = repoCtg.Read(x => x.Id < 5);
        }

        public void InsertTest()
        {

            repoUsr.Create(new User()
            {
                Name = "Test",
                Surname = "Test",
                Email = "Test@test.com",
                Active = true,
                Admin = false,
                Username = "testtest",
                Password = "1234",
                ActivationGuid = Guid.NewGuid(),
                RegisterationDate = DateTime.Now,
                UpdateDate = DateTime.Now.AddDays(1),
                UpdatedBy = "furkan"
            });
        }

        public void UpdateTest()
        {
            User usr = repoUsr.Find(x => x.Username == "testtest");
            if (usr != null)
            {
                usr.Username = "Test";
                repoUsr.Update(usr);
            }
        }

        public void DeleteTest()
        {
            User usr = repoUsr.Find(x => x.Username == "testtest");
            if (usr != null)
                repoUsr.Delete(usr);
        }

        public void InsertComment()
        {
            User usr = repoUsr.Find(x => x.Id == 1);
            Note note = repoNote.Find(x => x.Id == 1);
            repoComment.Create(new Comment()
            {
                User = usr,
                Note = note,
                UpdatedBy = usr.Username,
                RegisterationDate = DateTime.Now,
                UpdateDate = DateTime.Now.AddHours(1),
                Text = "Test comment"
            });
        }
    }
}
