using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Models
{
    public class AnimalType
    {
        public long AnimalTypeId { get; set; }
        public string Type { get; set; }
        public long PlaceId { get; set; }
    }
}
