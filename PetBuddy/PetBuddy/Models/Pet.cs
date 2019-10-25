namespace PetBuddy.Models
{
    public class Pet
    {
        public long PetId { get; set; } 
        public string PetName { get; set; }
        public string PetType { get; set; }
        public string PetDescription { get; set; }
        public string PetUri { get; set; }
        public string UserId { get; set; }
    }
}