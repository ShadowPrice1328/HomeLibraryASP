namespace BookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Year { get; set; }
        public string? Image { get; set; }
        public bool IsLent {  get; set; }
        public string? Source { get; set; }

        // Many-to-Many
        public List<Author>? Authors { get; set; }
        public List<Genre>? Genres { get; set; }
    }
}