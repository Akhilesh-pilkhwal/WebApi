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

namespace CrudAngular4WebApi.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private UserContext db = new UserContext();

        [HttpGet]
        [Route("getAllCategories")]
        public IHttpActionResult GetCategories()
        {
            return Ok(db.Categorys);
        }

       [HttpPost]
       [Route("getcategorybyid")]
       public IHttpActionResult GetCategory(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Product = db.Products.Where(x=>x.CategoryID==id).ToList();
            return Ok(category);
        }

        public IHttpActionResult PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.ID)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
        [Route("saveCategories")]
        public IHttpActionResult PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //foreach (var item in category.product)
            //{
                
            //    db.Products.Add(item); 
            //}
            //Category cat =new Category();
            //cat.categoryname=category.categoryname;
            db.Categorys.Add(category);
            db.SaveChanges();

            return Ok(category);
        }

       public IHttpActionResult DeleteCategory(int id)
        {
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categorys.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categorys.Count(e => e.ID == id) > 0;
        }
    }
}