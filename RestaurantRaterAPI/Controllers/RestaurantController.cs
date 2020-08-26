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
            List <Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            Restaurant restaurant = restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }
        // PUT
        [HttpPut]
                                                             // from Uri       // from body of request in Postman
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri]int id, [FromBody]Restaurant updatedRestaurant)
        {
            // check if restaurant is valid
            if (ModelState.IsValid)
            {
                // Find & update the specific restaurant
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);

                if (restaurant != null)
                {
                    // update restaurant
                    restaurant.Name = updatedRestaurant.Name;
                    //restaurant.Rating = updatedRestaurant.Rating;  -- DELETED bc we changed to no set 

                    // save changes to db
                    await _context.SaveChangesAsync();

                    return Ok("restaurant has been updated");
                }
                // didn't find the restaurant
                return NotFound();
            }
            // return bad request
            return BadRequest(ModelState);

        }
        // DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurantById(int id)
        {
            Restaurant restaurantEntity = await _context.Restaurants.FindAsync(id);

            if (restaurantEntity == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurantEntity);

            if (await _context.SaveChangesAsync() == 1) // checks to see if SaveChanges returns 1 item. Basically checks to see if you deleted it or not        
            {
                return Ok("restaurant deleted");
            }

            return InternalServerError();   // 500 error
        }
    }
}
