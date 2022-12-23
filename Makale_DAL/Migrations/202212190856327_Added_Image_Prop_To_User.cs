namespace Article_DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Added_Image_Prop_To_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Avatar", c => c.String(maxLength: 20));
            Sql("Update Users set Avatar='Avatar.jpg' ");
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "Avatar");
        }
    }
}
