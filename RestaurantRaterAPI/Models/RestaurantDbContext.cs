using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class RestaurantDbContext : DbContext    // Context
    {
                                         // Name "DefaultConnection" from Connection String in Web.config
        public RestaurantDbContext() : base("DefaultConnection") { }  // BASE CONSTRUCTOR

                // whole set of Restaurants -- table in the db
        public DbSet<Restaurant> Restaurants { get; set; } // Restaurant class
        public DbSet<Rating> Ratings { get; set; } // Rating class

    }
}