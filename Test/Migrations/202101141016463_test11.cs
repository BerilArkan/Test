namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test11 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
        }
    }
}
