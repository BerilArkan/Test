using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
     //   [ForeignKey("CategoryCurrent")]
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
       // [Required]
      //  public virtual Category CategoryCurrent { get; set; }

        // public ICollection<Category> CategoryCurrent { get; set; }
    }
}