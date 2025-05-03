using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private List<Book> _books;
        private List<Author> _authors;
        private List<Genre> _genres;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Book> Books { get; set; }
        public int BooksTotalCount { get; set; }

        public void OnGet()
        {
            _authors = new List<Author>()
            {
                new Author() { FirstName = "George", LastName = "Orwell" },
                new Author() { FirstName = "Taylor", LastName = "Jenkins-Reid" },
                new Author() { FirstName = "Andrzej", LastName = "Sapkowski" }
            };

            _genres = new List<Genre>()
            {
                new Genre() { Name = "Novel" },
                new Genre() { Name = "Dystopian" },
                new Genre() { Name = "Political fiction" },
                new Genre() { Name = "Fantasy" }
            };

            _books = new List<Book>()
            {
                new Book() 
                {
                    Title = "1984", 
                    Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell.",
                    Year = 1949,
                    Image = "1984.jpg",
                    IsLent = false,
                    Source = "Purchased",
                    Authors = _authors.Where(a => a.FirstName == "George" && a.LastName == "Orwell").ToList(),
                    Genres = _genres.Where(g => g.Name == "Novel" || g.Name == "Dystopian" || g.Name == "Political fiction").ToList()
                },
                new Book()
                {
                    Title = "The Seven Husbands of Evelyn Hugo",
                    Description = "A reclusive Hollywood icon reflects on her loves and losses, revealing her deepest secrets and regrets.",
                    Year = 2017,
                    Image = "EvelynHugo.jpg",
                    IsLent = false,
                    Source = "Purchased",
                    Authors = _authors.Where(a => a.FirstName == "Taylor" && a.LastName == "Jenkins-Reid").ToList(),
                    Genres = _genres.Where(g => g.Name == "Novel").ToList()
                },
                new Book()
                {
                    Title = "The Witcher: The Last Wish",
                    Description = "The Last Wish is the first book in Andrzej Sapkowski's bestselling fantasy series The Witcher, featuring the monster hunter Geralt.",
                    Year = 1993,
                    Image = "Witcher_LastWish.jpg",
                    IsLent = false,
                    Source = "Purchased",
                    Authors = _authors.Where(a => a.FirstName == "Andrzej" && a.LastName == "Sapkowski").ToList(),
                    Genres = _genres.Where(g => g.Name == "Fantasy").ToList()
                },
                new Book()
                {
                    Title = "Brave New World",
                    Description = "A dystopian novel set in a totalitarian society where citizens are conditioned to maintain a perfect order.",
                    Year = 1932,
                    Image = "BraveNewWorld.jpg",
                    IsLent = true,
                    Source = "Borrowed",
                    Authors = _authors.Where(a => a.FirstName == "Aldous" && a.LastName == "Huxley").ToList(),
                    Genres = _genres.Where(g => g.Name == "Dystopian").ToList()
                },
                new Book()
                {
                    Title = "The Name of the Wind",
                    Description = "A young man tells the story of his life, from childhood to the present day, in a world full of magic, mystery, and danger.",
                    Year = 2007,
                    Image = "NameOfTheWind.jpg",
                    IsLent = false,
                    Source = "Purchased",
                    Authors = _authors.Where(a => a.FirstName == "Patrick" && a.LastName == "Rothfuss").ToList(),
                    Genres = _genres.Where(g => g.Name == "Fantasy").ToList()
                }
            };
            
            Books = _books.Take(5).ToList();
            BooksTotalCount = _books.Count;
        }
    }
}
