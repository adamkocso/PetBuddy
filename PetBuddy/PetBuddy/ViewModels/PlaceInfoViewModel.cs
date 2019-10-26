using Microsoft.AspNetCore.Http;
using PetBuddy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetBuddy.ViewModels
{
    public class PlaceInfoViewModel
    {
        public User User { get; set; }
        public Place Place { get; set; }
        public Review Review { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "The Price field is required.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }
        [Required]
        public string PetType { get; set; }
        public string PlaceUri { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public IFormFile File { get; set; }
    }
}
