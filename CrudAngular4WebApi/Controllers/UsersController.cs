using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrudAngular4WebApi;
using CrudAngular4WebApi.Entity;
using System.Web;
using System.IO;

namespace CrudAngular4WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private UserContext db = new UserContext();
        [HttpPost]
        [Route("uploadFile")]
        public IHttpActionResult UploadJsonFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;

            Files LocalFile = new Files();
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    LocalFile.filename = postedFile.FileName;
                }
            }
            return Ok(LocalFile);
        }
        // GET: api/Users
        [HttpGet]
        [Route("getAllUser")]
        public IHttpActionResult GetUsers()
        {
            return Ok(db.Users);
        }

        // GET: api/Users/5
        [HttpPost]
        [Route("getUserById")]
        public IHttpActionResult getUserById(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPost]
        [Route("editUser")]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/authenticatedUser")]
        private IHttpActionResult AuthenticatedUserPost(UserRegister user)
        {
            var isAuthenticated = db.Users.Count(e => e.email == user.email && e.password == user.password) > 0;
            if (isAuthenticated)
            {
                return BadRequest();
            }
            else
            {
                return Ok(isAuthenticated);
            }
        }

        // POST: api/Users
        [HttpPost]
        [Route("api/saveUser")]
        public IHttpActionResult Saveuser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return Ok(user);
        }


        // DELETE: api/Users/5
        [HttpPost]
        [Route("deleteUser")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }


    }
}