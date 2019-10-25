using PetBuddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.ViewModels
{
    public class PlaceInfoViewModel
    {
        public User User { get; set; }
        public Place Place { get; set; }
        public Review Review { get; set; }
    }
}
