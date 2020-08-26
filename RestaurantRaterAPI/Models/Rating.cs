using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        // PRIMARY KEY
        [Key]
        public int Id { get; set; } // can use Id in a different class

        // FOREIGN KEY (Restaurant Key) -- connect to ONE restaurant in Restaurant Table
        //[ForeignKey("Restaurant")] // "Restaurant" matches name of Navigation Property Name (Restaurant)
        [ForeignKey(nameof(Restaurant))] // nameOf automatically gets name of Navigation Property instead of inputting a string -- it updates automatically
        public int RestaurantId { get; set; }
        // FOREIGN KEY NAVIGATION PROPERTY -- tells us which db table to go to to get the correct info
            // virtual keyword
        public virtual Restaurant Restaurant { get; set; }


        [Required]
        [Range(0, 10)] // the range saved into the db
        public double FoodScore { get; set; }
        [Required, Range(0,10)]
        public double EnvironmentScore { get; set; }
        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }
        
        public double AverageRating  // Add all scores & get the average out of 10
        { 
            get
            {
                var totalScore = FoodScore + EnvironmentScore + CleanlinessScore;
                return totalScore / 3;
            } 
        }
    }
}