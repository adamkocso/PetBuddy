using System.ComponentModel.DataAnnotations;
using PetBuddy.Models;

namespace PetBuddy.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Please provide your city!")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Please provide your name!")]
        public string Name { get; set; }
    }
}