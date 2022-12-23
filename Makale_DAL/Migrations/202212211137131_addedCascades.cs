namespace Article_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCascades : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Note_Id", "dbo.Notes");
            DropForeignKey("dbo.Likes", "Note_Id", "dbo.Notes");
            DropIndex("dbo.Comments", new[] { "Note_Id" });
            DropIndex("dbo.Likes", new[] { "Note_Id" });
            AlterColumn("dbo.Comments", "Note_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Likes", "Note_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Note_Id");
            CreateIndex("dbo.Likes", "Note_Id");
            AddForeignKey("dbo.Comments", "Note_Id", "dbo.Notes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Likes", "Note_Id", "dbo.Notes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "Note_Id", "dbo.Notes");
            DropForeignKey("dbo.Comments", "Note_Id", "dbo.Notes");
            DropIndex("dbo.Likes", new[] { "Note_Id" });
            DropIndex("dbo.Comments", new[] { "Note_Id" });
            AlterColumn("dbo.Likes", "Note_Id", c => c.Int());
            AlterColumn("dbo.Comments", "Note_Id", c => c.Int());
            CreateIndex("dbo.Likes", "Note_Id");
            CreateIndex("dbo.Comments", "Note_Id");
            AddForeignKey("dbo.Likes", "Note_Id", "dbo.Notes", "Id");
            AddForeignKey("dbo.Comments", "Note_Id", "dbo.Notes", "Id");
        }
    }
}
