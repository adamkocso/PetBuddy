using System.ComponentModel.DataAnnotations;
using PetBuddy.Models;

namespace PetBuddy.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Gimmi your name!")]
        public string City { get; set; }
        
        [Required(ErrorMessage = "Gimmi your city!")]
        public string Name { get; set; }
        
    }
}