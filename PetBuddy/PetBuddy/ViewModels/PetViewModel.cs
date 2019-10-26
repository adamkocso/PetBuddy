using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PetBuddy.ViewModels
{
    public class PetViewModel
    {
        [Required]
        [Display(Name = "Pet Name")]
        public string PetName { get; set; }
        [Required]
        [Display(Name = "Pet Type")]
        public string PetType { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string PetDescription { get; set; }
        public IFormFile File { get; set; }
    }
}