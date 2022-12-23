using Article_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article_DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.SetInitializer(new DBCreator());
        }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            //Fluent API
			modelBuilder.Entity<Note>().HasMany(n=>n.Comments).WithRequired(c=>c.Note).WillCascadeOnDelete(true);
            modelBuilder.Entity<Note>().HasMany(n=> n.Likes).WithRequired(l=>l.Note).WillCascadeOnDelete(true);
		}
		public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Like> Likes { get; set; }

    }
}
