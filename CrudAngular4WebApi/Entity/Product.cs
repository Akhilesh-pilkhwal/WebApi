using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAngular4WebApi.Entity
{
    public class Product
    {
        public int ID { get; set; }
        public string productname { get; set; }
        public int price { get; set; }
        public int? CategoryID { get; set; }
        //public virtual  Category Category { get; set; }

        
    }
}