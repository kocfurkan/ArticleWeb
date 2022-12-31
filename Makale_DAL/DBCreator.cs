using Article_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_DAL
{
    public class DBCreator : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {

            User admin = new User()
            {
                Name = "Furkan",
                Surname = "Koc",
                Email = "furkab@mail.com",
                Password = "1234",
                Active = true,
                Admin = true,
                ActivationGuid = Guid.NewGuid(),
                Username = "furkan",
                RegisterationDate = DateTime.Now,
                UpdateDate = DateTime.Now.AddMinutes(5),
                UpdatedBy = "furkan"
            };
            context.Users.Add(admin);
            context.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                User user = new User()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    Password = "1234",
                    Active = true,
                    Admin = false,
                    ActivationGuid = Guid.NewGuid(),
                    Username = "user" + i,
                    RegisterationDate = DateTime.Now,
                    UpdateDate = DateTime.Now.AddMinutes(5),
                    UpdatedBy = "user" + i,
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
            context.SaveChanges();
            List<User> userList = context.Users.ToList();

            for (int i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    RegisterationDate = DateTime.Now,
                    UpdatedBy = "furkan",
                    UpdateDate = DateTime.Now.AddMinutes(5),
                };
                context.Categories.Add(category);


                for (int j = 0; j < 3; j++)
                {
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(5),
                        Text = FakeData.TextData.GetSentences(3),
                        Draft = false,
                        LikeNumber = FakeData.NumberData.GetNumber(1, 4),
                        User = userList[FakeData.NumberData.GetNumber(0, 5)],
                        RegisterationDate = DateTime.Now,
                        UpdateDate = DateTime.Now.AddMinutes(5),
                        UpdatedBy = "user" + j,
                    };
                    category.Notes.Add(note);

                    for (int k = 0; k < 3; k++)
                    {
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentences(3),
                            User = userList[FakeData.NumberData.GetNumber(0, 5)],
                            RegisterationDate = DateTime.Now,
                            UpdateDate = DateTime.Now.AddMinutes(5),
                            UpdatedBy = userList[FakeData.NumberData.GetNumber(0, 5)].Username,
                        };
                        note.Comments.Add(comment);
                    }
                    for (int f = 0; f < note.LikeNumber; f++)
                    {
                        Like like = new Like()
                        {
                            User = userList[FakeData.NumberData.GetNumber(1, 6)],
                        };
                        note.Likes.Add(like);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
