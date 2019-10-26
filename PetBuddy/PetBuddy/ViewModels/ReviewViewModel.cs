using System.ComponentModel.DataAnnotations;
using PetBuddy.Models;

namespace PetBuddy.ViewModels
{
    public class ReviewViewModel
    {
        [Required] 
        public int Rating { get; set; }
        [Required]
        public string Text { get; set; }
        public Place Place { get; set; }
        public User User { get; set; }
    }
}