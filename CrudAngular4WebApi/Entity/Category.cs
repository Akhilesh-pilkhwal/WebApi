using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAngular4WebApi.Entity
{
    public class Category
    {
        public int ID { get; set; }
        public string categoryname { get; set; }
        //relation
        public ICollection<Product> Product { get; set; }
    }
    
}