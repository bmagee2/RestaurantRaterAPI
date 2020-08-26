using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    // Restaurant ENTITY (class that's stored in the db)
    public class Restaurant
    {
        // Primary Key
        [Key] // KEY ATTRIBUTE -- always required by default
        public int Id { get; set; }
        [Required] // a restuarant name will always be required to add to the database
        public string Name { get; set; }
        //[Required] // doesn't need to be required anymore because it's readonly -- no set
        public double Rating 
        { 
            get 
            {
                //return FoodRating + EnvironmentRating + CleanlinessScore / 3;  -- this could replace foreach 

                // Calculate total average score based on Ratings
                double totalAverageRating = 0;

                // Add all Ratings together to get total Average Rating
                foreach (var rating in Ratings)
                {
                    totalAverageRating = totalAverageRating + rating.AverageRating; // OR totalAverageRating += rating.AverageRating;
                }
                     // checking to make sure greater than 0  // total sum of Ratings
                return (Ratings.Count > 0) ? totalAverageRating / Ratings.Count : 0;
            } 
        }

        // Average Food Rating
        //public double AverageFoodRating 
        //{ 
        //    get 
        //    {
        //        double totalAverageFoodRating = 0;

        //        foreach (var rating in Ratings)
        //        {
        //            totalAverageFoodRating += rating.AverageRating;
        //        }

        //        return (Ratings.Count > 0) ? totalAverageFoodRating / Ratings.Count : 0;
        //    } 
        //}

        // JOSH'S EXAMPLE
        public double FoodRating
        {

            get 
                {
                    double totalFoodScore = 0;

                    foreach (var rating in Ratings)
                    {
                        totalFoodScore += rating.FoodScore;
                    }

                    return (Ratings.Count > 0) ? totalFoodScore / Ratings.Count : 0;
                }
        }

        // Average Environment Rating
        //public double AverageEnvironmentRating
        //{
        //    get
        //    {
        //        double totalAverageEnvironmentRating = 0;

        //        foreach (var rating in Ratings)
        //        {
        //            totalAverageEnvironmentRating += rating.AverageRating;
        //        }

        //        return (Ratings.Count > 0) ? totalAverageEnvironmentRating / Ratings.Count : 0;
        //    }
        //}


        // JOSH'S EXAMPLE
        public double EnvironmentRating
        {
            get
            {
               IEnumerable<double> scores = Ratings.Select(rating => rating.EnvironmentScore);

                double totalEnvironmentScore = scores.Sum();

                return (Ratings.Count > 0) ? totalEnvironmentScore / Ratings.Count : 0;
            }
        }

        // Average Cleanliness Rating
        //public double AverageCleanlinessRating
        //{
        //    get
        //    {
        //        double totalAverageCleanlinessRating = 0;

        //        foreach (var rating in Ratings)
        //        {
        //            totalAverageCleanlinessRating += rating.AverageRating;
        //        }

        //        return (Ratings.Count > 0) ? totalAverageCleanlinessRating / Ratings.Count : 0;
        //    }
        //}

        // JOSH'S EXAMPLE
        public double CleanlinessScore
        {
            get
            {
                var totalScore = Ratings.Select(r => r.CleanlinessScore).Sum();
                return (Ratings.Count > 0) ? totalScore / Ratings.Count : 0;
                // return (Ratings.Count > 0) ? Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count : 0;
            }
        }

        public bool IsRecommended => Rating >= 8.5;
        //{
        //    get
        //    {
        //        return Rating > 3.5;
        //        //return (Rating > 3.5) ? true : false;
        //    }
        //}

        // Find all associated Rating objects (all properties in Rating Class) from the db & store in Ratings List (based on Foreign Key relationship) -- Restaurant class has many Ratings
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    }
}