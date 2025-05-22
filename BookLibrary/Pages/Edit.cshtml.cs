using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;

namespace BookLibrary.Pages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private List<Book> _books;
    private List<Author> _authors;
    private List<Genre> _genres;
    private readonly AppDbContext _appDbContext;

    public EditModel(ILogger<EditModel> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
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

        _books = _appDbContext.Books.ToList();
        
        Book? _book = _books.FirstOrDefault(b => b.IdBook == id);

        if (_book == null) 
            return NotFound();

        Book = _book;

        return Page();
    }
}