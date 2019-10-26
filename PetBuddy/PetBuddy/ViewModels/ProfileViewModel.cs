using System.Collections.Generic;
using PetBuddy.Models;

namespace PetBuddy.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Pet> Pets { get; set; }
    }
}