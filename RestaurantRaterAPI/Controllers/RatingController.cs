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
    public class RatingController : ApiController
    {
        // RestaurantDbContext Field
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        // Create new ratings
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            // check to see if the model is NOT valid --  because we only want to continue if the restaurant we want to rate is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find restaurant you're trying to rate
            var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurant == null)
            {
                return BadRequest($"target restaurant with the ID of {model.RestaurantId} doesn't exist");
            }

            // restaurant isn't null so we can rate it
            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"you rated {restaurant.Name} successfully!");
            }
            return InternalServerError();
        }


        // Get ratings for a specific restaurant by id
        [HttpGet]
        public async Task<IHttpActionResult> GetRatingsByRestaurantByID(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant.Ratings); // or no .Ratings
            }
            return NotFound();
        }

        //[HttpGet]
        //public async Task<IHttpActionResult> GetRatingsByRestaurantId(int id)
        //{
        //    List<Rating> rating = await _context.Ratings.Where(r => r.RestaurantId == id).ToListAsync();

        //    return Ok(rating);
        //}

        // Get all ratings for all restaurants ?
        //[HttpGet]
        //public async Task<IHttpActionResult> GetAllRatingsForAllRestaurants()
        //{
        //    List<Rating> ratings = await _context.Ratings.ToListAsync();
        //    return Ok(ratings);
        //}

        // Update ratings for one specific restaurant -- this doesn't work
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRatings([FromUri] int id, [FromBody] Rating updatedRating)
        {
            // check if restaurant is valid
            if (ModelState.IsValid)
            {
                // Find & update the specific restaurant
                Rating rating = await _context.Ratings.FindAsync(id);

                if (rating != null)
                {
                    // update ratings
                    rating.FoodScore = updatedRating.FoodScore;
                    rating.EnvironmentScore = updatedRating.EnvironmentScore;
                    rating.CleanlinessScore = updatedRating.CleanlinessScore;

                    // save changes to db
                    await _context.SaveChangesAsync();

                    return Ok("restaurant has been updated");
                }
                // didn't find 
                return NotFound();
            }
            // return bad request
            return BadRequest(ModelState);

        }

        // Delete ratings for one particular restaurant
        //[HttpDelete]
        //public async Task<IHttpActionResult> DeleteRestaurantRatings()
        //{

        //}
    }
}
