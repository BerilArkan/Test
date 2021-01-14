namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products123 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_id" });
            RenameColumn(table: "dbo.Products", name: "Category_id", newName: "CategoryId");
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "id", cascadeDelete: true);
            AlterStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        CategoryId = p.Int(),
                        ProductName = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Products]([CategoryId], [ProductName])
                      VALUES (@CategoryId, @ProductName)
                      
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
                        CategoryId = p.Int(),
                        ProductName = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [CategoryId] = @CategoryId, [ProductName] = @ProductName
                      WHERE ([id] = @id)"
            );
            
            AlterStoredProcedure(
                "dbo.Product_Delete",
                p => new
                    {
                        id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Products]
                      WHERE ([id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_id");
            CreateIndex("dbo.Products", "Category_id");
            AddForeignKey("dbo.Products", "Category_id", "dbo.Categories", "id");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
