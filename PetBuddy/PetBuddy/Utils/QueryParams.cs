using System;
using System.Collections.Generic;
using PetBuddy.Models;

namespace PetBuddy.Utils
{
    public class QueryParams
    {
        public int Price { get; set; }
        public string AnimalType { get; set; }
        public string City { get; set; }
    }
}