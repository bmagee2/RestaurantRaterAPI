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
        [Key] // KEY ATTRIBUTE -- always required by default
        public int Id { get; set; }
        [Required] // a restuarant name will always be required to add to the database
        public string Name { get; set; }
        // a rating will always be required to add to the database
        public double Rating { get; set; }
        public bool IsRecommended => Rating > 3.5;
        //{
        //    get
        //    {
        //        return Rating > 3.5;
        //        //return (Rating > 3.5) ? true : false;
        //    }
        //}
    }
}