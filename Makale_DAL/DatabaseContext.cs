using Makale_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makale_DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.SetInitializer(new DBCreator());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Like> Likes { get; set; }

    }
}
