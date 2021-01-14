using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Category
    {
        [Key]

        public int id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
       // public ICollection<Product> IProducts { get; set; }
     //   public virtual ICollection<Product> ProductCurrent { get; set; }
    }
}