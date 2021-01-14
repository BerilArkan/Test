namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category_id", c => c.Int());
            CreateIndex("dbo.Products", "Category_id");
            AddForeignKey("dbo.Products", "Category_id", "dbo.Categories", "id");
            DropColumn("dbo.Products", "CategoryId");
            AlterStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        ProductName = p.String(),
                        Category_id = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Products]([ProductName], [Category_id])
                      VALUES (@ProductName, @Category_id)
                      
                      DECLARE @id int
                      SELECT @id = [id]
                      FROM [dbo].[Products]
                      WHERE @@ROWCOUNT > 0 AND [id] = scope_identity()
                      
                      SELECT t0.[id]
                      FROM [dbo].[Products] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[id] = @id"
            );
            
            AlterStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        id = p.Int(),
                        ProductName = p.String(),
                        Category_id = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [ProductName] = @ProductName, [Category_id] = @Category_id
                      WHERE ([id] = @id)"
            );
            
            AlterStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        id = p.Int(),
                        Category_id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Products]
                      WHERE (([id] = @id) AND (([Category_id] = @Category_id) OR ([Category_id] IS NULL AND @Category_id IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "CategoryId", c => c.String());
            DropForeignKey("dbo.Products", "Category_id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_id" });
            DropColumn("dbo.Products", "Category_id");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
