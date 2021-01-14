using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Migrations;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly TextContext textContext = new TextContext();

        public ActionResult Index()
        {
            return View();
        }
         public JsonResult SetProductList(string category, string product)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                category = null;
            }
            if (string.IsNullOrWhiteSpace(product))
            {
                product = null;
            }
            try
            {

                using (TextContext dc = new TextContext())
                {
                    //var product = new Product { ProductName = product }; 

                    int ctId = dc.Categories
                              .Where(c => c.CategoryName == category)
                              .Select(c => c.CategoryId)
                              .SingleOrDefault();

                    var setValue = new Product { ProductName = product, CategoryId = ctId };
                    dc.Products.Add(setValue);

                    dc.SaveChanges();
                }
                 
                return Json(new
                {
                    success = true,
                    message = "Kategori Başarıyla Eklendi!"
                },
                JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return null;

            }
             
        }

        public JsonResult GetProductCategoryList()
        { 

            TextContext db = new TextContext();
            var dataDB = (from app in db.Categories
                          join service in db.Products on app.CategoryId equals service.CategoryId
                          where app.CategoryId == service.CategoryId
                          select new IAViewModel
                          {
                              CategoryName = app.CategoryName,
                              ProductName = service.ProductName,
                          }).ToList();

            return Json(new { success = true, data = dataDB }, JsonRequestBehavior.AllowGet);

            //List<Product> products = new List<Product>()
            //{
            //    new Product(){ProductName= ""}
            //};

            //foreach (var item in products)
            //{
            //    db.Products.Add(item);
            //}
            ////db.Products.Add(p);
            //db.SaveChanges();
            //db.Products.Add(p);
            //db.SaveChanges();

            //List<Product> productList = new List<Product>();
            //using (var context = new TextContext())
            //{
            //    var query = context.dataContext.FirstOrDefault<Product>();
            //}

            //  List<Product> productList = textContext.dataContext.ToList();
             
             
        }
        //Kategori ekleniyor.
        public JsonResult SetProductCategoryList(string category)
        {

            if (string.IsNullOrWhiteSpace(category))
            {
                category = null;
            }

            try
            {
                TextContext db = new TextContext();
                var getId = db.Categories
                       .OrderByDescending(p => p.id)
                       .FirstOrDefault();

                List<Category> categories = new List<Category>()
            {
                new Category(){CategoryName= category}
            };

                foreach (var item in categories)
                {
                    if (getId.CategoryId == 0)
                        item.CategoryId = 0;
                    else
                        item.CategoryId = getId.CategoryId + 1;

                    db.Categories.Add(item);

                }
                db.SaveChanges();


                return Json(new
                {
                    success = true,
                    message = "Kategori Başarıyla Eklendi!"
                },
                JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            { 
                return null;
            }            

        }

        //Ürün adı güncelleme
        public JsonResult UpdateProductInfo(string productName, string beforeProductName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                productName = null;
            }

            try
            {
                using (var db = new TextContext())
                {
                    var result = db.Products.SingleOrDefault(b => b.ProductName == beforeProductName);

                    if (result != null)
                    {
                        result.ProductName = productName;
                        db.SaveChanges();

                    }
                } 
                return Json(new
                {
                    success = true,
                    message = "Ürün Adı Güncellendi!"
                },
                JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        //Ürünü silme 
        public JsonResult DeleteProduct(string product)
        {
 
            if (string.IsNullOrWhiteSpace(product))
            {
                product = null;
            }
            try
            { 

                using (var dbContext = new TextContext())
                {
                    var rmvRec = dbContext.Products.FirstOrDefault(x => x.ProductName == product); 
                    dbContext.Products.Remove(rmvRec);
                    dbContext.SaveChanges();
                }

                return Json(new
                {
                    success = true,
                    message = product + " Ürünü Silindi!"
                },
                JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult GetDBCategoryList()
        {
            TextContext context = new TextContext();
            var ürünler = context.Categories.ToList();

            //foreach (var item in ürünler)
            //{
            //    Category category = new Category();
            //    category.CategoryName = item.CategoryName;
            //    ürünler.Add(category);
            //} 

            return Json(new
            {
                success = true,
                data = ürünler

            }, JsonRequestBehavior.AllowGet);



        }
    }
    //Ürün Adı Güncelleme

    internal class IAViewModel
    {
        public object CategoryName { get; set; }
        public object ProductName { get; set; }
    }
}