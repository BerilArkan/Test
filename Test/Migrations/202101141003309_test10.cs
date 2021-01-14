namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test10 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Categories", "id");
        }
    }
}
