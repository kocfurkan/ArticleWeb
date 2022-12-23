namespace Article_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryIdToNotes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Notes", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Notes", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Notes", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notes", "CategoryId");
            AddForeignKey("dbo.Notes", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Notes", new[] { "CategoryId" });
            AlterColumn("dbo.Notes", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Notes", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Notes", "Category_Id");
            AddForeignKey("dbo.Notes", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
