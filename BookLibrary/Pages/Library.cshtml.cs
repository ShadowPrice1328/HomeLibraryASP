using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using BookLibrary.Data;

namespace BookLibrary.Pages;

public class LibraryModel : PageModel
{
    private readonly ILogger<LibraryModel> _logger;
    private List<Book> _books;
    private List<Author> _authors;
    private List<Genre> _genres;
    private readonly AppDbContext _appDbContext;
    public LibraryModel(ILogger<LibraryModel> logger, AppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    public List<Book> Books { get; set; }
    public int BooksTotalCount { get; set; }

    public void OnGet()
    {
        _authors = _appDbContext.Authors.ToList();

        _genres = _appDbContext.Genres.ToList();

        _books = _appDbContext.Books.ToList();
        
        Books = _books;
        BooksTotalCount = _books.Count;
    }
}