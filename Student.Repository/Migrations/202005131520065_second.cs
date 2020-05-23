namespace Student.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Image", c => c.Binary(storeType: "image"));
        }
    }
}
