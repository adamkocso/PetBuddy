using System.Collections.Generic;
using System.Security.Principal;
using PetBuddy.Models;
using PetBuddy.Utils;

namespace PetBuddy.ViewModels
{
    public class HomeViewModel
    {
        public User User { get; set; }
        public QueryParams QueryParams { get; set; }
        public List<Place> Places { get; set; }
    }
}