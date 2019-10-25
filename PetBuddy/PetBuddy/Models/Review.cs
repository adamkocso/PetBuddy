namespace PetBuddy.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public long PlaceId { get; set; }
        public string UserId { get; set; }
    }
}