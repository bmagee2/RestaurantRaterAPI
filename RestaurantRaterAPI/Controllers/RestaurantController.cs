using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    // POSTMAN
    public class RestaurantController : ApiController
    {
        // need instance of RestaurantDbContext
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // POST
        [HttpPost] // must be a Post
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("your request body can't be empty");
            }
            if (ModelState.IsValid) // if Restaurant model is valid
            {
                //// need instance of RestaurantDbContext
                //RestaurantDbContext context = new RestaurantDbContext();
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();
                return Ok("restaurant created & saved!");
            }

            return BadRequest(ModelState);
        }

        // GET all
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        // GET by Id
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }
        // PUT

        // DELETE
    }
}
