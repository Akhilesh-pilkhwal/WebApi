using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudAngular4WebApi.Entity
{
    public class User
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string language { get; set; }
        public string password { get; set; }
    }
    public class UserRegister {

        public string email { get; set; }
        public string password { get; set; }
    
    }

    public class Files {
        public string filename { get; set; }
    }
}