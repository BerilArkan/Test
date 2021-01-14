using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class TextContext : DbContext
    {
        public TextContext() : base("testConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        // public DbSet<Product> dataContext { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().MapToStoredProcedures();


            //modelBuilder.Entity<Product>()
            //.HasRequired(c => c.CategoryCurrent)
            //.WithMany(p => p.ProductCurrent)
            //.HasForeignKey(c => c.CategoryId);

            //    modelBuilder.Entity<Product>()
            //.HasRequired<Category>(b => b.CategoryCurrent)
            //.WithMany(a => a.ProductCurrent)
            //.HasForeignKey<int>(b => b.CategoryId);

            //modelBuilder.Entity<Category>()
            //    .HasOptional(s => s.ProductCurrent)
            //    .WithRequired(a => a.CategoryCurrent);


            //      modelBuilder.Entity<Category>()
            //.HasMany<Product>(g => g.IProducts)
            //.WithRequired(s => s.CategoryCurrent)
            //.HasForeignKey<int>(s => s.CategoryId);
        }

    }
}