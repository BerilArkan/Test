namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test9 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "id");
        }
    }
}
