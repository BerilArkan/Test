namespace Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Product_Insert",
                p => new
                    {
                        CategoryId = p.String(),
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
            
            CreateStoredProcedure(
                "dbo.Product_Update",
                p => new
                    {
                        id = p.Int(),
                        CategoryId = p.String(),
                        ProductName = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Products]
                      SET [CategoryId] = @CategoryId, [ProductName] = @ProductName
                      WHERE ([id] = @id)"
            );
            
            CreateStoredProcedure(
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
            DropStoredProcedure("dbo.Product_Delete");
            DropStoredProcedure("dbo.Product_Update");
            DropStoredProcedure("dbo.Product_Insert");
        }
    }
}
