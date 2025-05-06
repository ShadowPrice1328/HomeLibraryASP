using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Pages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private List<Book> _books;
    private List<Author> _authors;
    private List<Genre> _genres;
    public EditModel(ILogger<EditModel> logger)
    {
        _logger = logger;
    }

    public Book Book { get; set; }
    public IActionResult OnGet(int id)
    {
        _authors = new List<Author>()
        {
            new Author() { FirstName = "George", LastName = "Orwell" },
            new Author() { FirstName = "Taylor", LastName = "Jenkins-Reid" },
            new Author() { FirstName = "Andrzej", LastName = "Sapkowski" },
            new Author() { FirstName = "Aldous", LastName = "Huxley" },
            new Author() { FirstName = "Patrick", LastName = "Rothfuss" },
            new Author() { FirstName = "J.K.", LastName = "Rowling" },
            new Author() { FirstName = "J.R.R.", LastName = "Tolkien" },
            new Author() { FirstName = "Harper", LastName = "Lee" },
            new Author() { FirstName = "Gabriel", LastName = "García Márquez" },
            new Author() { FirstName = "F. Scott", LastName = "Fitzgerald" }
        };

        _genres = new List<Genre>()
        {
            new Genre() { Name = "Novel" },
            new Genre() { Name = "Dystopian" },
            new Genre() { Name = "Political fiction" },
            new Genre() { Name = "Fantasy" },
            new Genre() { Name = "Classic" },
            new Genre() { Name = "Adventure" },
            new Genre() { Name = "Romance" }
        };

        _books = new List<Book>()
        {
            new Book() 
            {
                Id = 1,
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
                Id = 2,
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
                Id = 3,
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
                Id = 4,
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
                Id = 5,
                Title = "The Name of the Wind",
                Description = "A young man tells the story of his life, from childhood to the present day, in a world full of magic, mystery, and danger.",
                Year = 2007,
                Image = "NameOfTheWind.jpg",
                IsLent = false,
                Source = "Purchased",
                Authors = _authors.Where(a => a.FirstName == "Patrick" && a.LastName == "Rothfuss").ToList(),
                Genres = _genres.Where(g => g.Name == "Fantasy").ToList()
            },
            new Book()
            {
                Id = 6,
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "A young wizard discovers his magical heritage and begins his journey at Hogwarts School of Witchcraft and Wizardry.",
                Year = 1997,
                Image = "HarryPotter1.jpg",
                IsLent = false,
                Source = "Gifted",
                Authors = _authors.Where(a => a.FirstName == "J.K." && a.LastName == "Rowling").ToList(),
                Genres = _genres.Where(g => g.Name == "Fantasy").ToList()
            },
            new Book()
            {
                Id = 7,
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                Description = "The epic adventure of a hobbit and his friends to destroy the One Ring and save Middle-earth.",
                Year = 1954,
                Image = "LOTR_Fellowship.jpg",
                IsLent = false,
                Source = "Purchased",
                Authors = _authors.Where(a => a.FirstName == "J.R.R." && a.LastName == "Tolkien").ToList(),
                Genres = _genres.Where(g => g.Name == "Fantasy" || g.Name == "Adventure").ToList()
            },
            new Book()
            {
                Id = 8,
                Title = "To Kill a Mockingbird",
                Description = "A novel about the injustices of racial prejudice and moral growth in the Deep South.",
                Year = 1960,
                Image = "ToKillAMockingbird.jpg",
                IsLent = false,
                Source = "Purchased",
                Authors = _authors.Where(a => a.FirstName == "Harper" && a.LastName == "Lee").ToList(),
                Genres = _genres.Where(g => g.Name == "Novel" || g.Name == "Classic").ToList()
            },
            new Book()
            {
                Id = 9,
                Title = "One Hundred Years of Solitude",
                Description = "A multi-generational tale of the Buendía family and the mythical town of Macondo.",
                Year = 1967,
                Image = "OneHundredYearsOfSolitude.jpg",
                IsLent = false,
                Source = "Purchased",
                Authors = _authors.Where(a => a.FirstName == "Gabriel" && a.LastName == "García Márquez").ToList(),
                Genres = _genres.Where(g => g.Name == "Novel").ToList()
            },
            new Book()
            {
                Id = 10,
                Title = "The Great Gatsby",
                Description = "A tragic love story set in the Jazz Age, exploring themes of wealth and the American Dream.",
                Year = 1925,
                Image = "TheGreatGatsby.jpg",
                IsLent = false,
                Source = "Borrowed",
                Authors = _authors.Where(a => a.FirstName == "F. Scott" && a.LastName == "Fitzgerald").ToList(),
                Genres = _genres.Where(g => g.Name == "Novel").ToList()
            }
        };

        Book? _book = _books.FirstOrDefault(b => b.Id == id);

        if (_book == null) 
            return NotFound();

        Book = _book;

        return Page();
    }
}