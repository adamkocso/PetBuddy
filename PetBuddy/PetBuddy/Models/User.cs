using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.Models
{
    public class User : IdentityUser
    {
        
        public string UserUri { get; set; }
        [Required(ErrorMessage = "Please provide your city.")]
        public string City { get; set; }
        public List<Pet> Pets { get; set; }
        public long PlaceId { get; set; }
    }
}