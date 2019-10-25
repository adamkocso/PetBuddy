using System;
using System.Collections.Generic;

namespace PetBuddy.Models
{
    public class Place
    {
        public long PlaceId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<AnimalType> Animals { get; set; } = null;
        public int Price { get; set; }
        public double AverageRating { get; set; } = 0;
        public string Description { get; set; }
        public string PlaceUri { get; set; }
        public List<Review> Reviews { get; set; } = null;
        public string UserId { get; set; }
    }
}