using CrudAngular4WebApi.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudAngular4WebApi
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=userconnectionstring")
       {
 
       }
       public IDbSet<User> Users { get; set;  }
       public IDbSet<Category> Categorys { get; set; }
       public IDbSet<Product> Products { get; set; }

       //public System.Data.Entity.DbSet<CrudAngular4WebApi.Entity.Category> Categories { get; set; }
    }
}