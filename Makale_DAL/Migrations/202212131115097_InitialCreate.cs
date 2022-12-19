namespace Makale_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 150),
                        RegisterationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Text = c.String(nullable: false, maxLength: 250),
                        Draft = c.Boolean(nullable: false),
                        LikeNumber = c.Int(nullable: false),
                        RegisterationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 20),
                        Category_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 250),
                        RegisterationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 20),
                        Note_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.Note_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Note_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Surname = c.String(maxLength: 20),
                        Username = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 20),
                        Active = c.Boolean(nullable: false),
                        Admin = c.Boolean(nullable: false),
                        ActivationGuid = c.Guid(nullable: false),
                        RegisterationDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Note_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.Note_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Note_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Likes", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Likes", "Note_Id", "dbo.Notes");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Note_Id", "dbo.Notes");
            DropForeignKey("dbo.Notes", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Likes", new[] { "User_Id" });
            DropIndex("dbo.Likes", new[] { "Note_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Note_Id" });
            DropIndex("dbo.Notes", new[] { "User_Id" });
            DropIndex("dbo.Notes", new[] { "Category_Id" });
            DropTable("dbo.Likes");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
            DropTable("dbo.Notes");
            DropTable("dbo.Categories");
        }
    }
}
