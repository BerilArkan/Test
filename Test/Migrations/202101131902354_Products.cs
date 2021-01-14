namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryName", c => c.String());
            DropColumn("dbo.Categories", "ProductName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ProductName", c => c.String());
            DropColumn("dbo.Categories", "CategoryName");
        }
    }
}
