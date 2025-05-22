using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;

namespace BookLibrary.Pages;

public class DetailsModel : PageModel
{
    private readonly ILogger<DetailsModel> _logger;
    private List<Book> _books;
    private List<Author> _authors;
    private List<Genre> _genres;
    private readonly AppDbContext _appDbContext;
    public DetailsModel(ILogger<DetailsModel> logger, AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    public Book Book { get; set; }
    public IActionResult OnGet(int id)
    {
        _authors = _appDbContext.Authors.ToList();
        _genres = _appDbContext.Genres.ToList();

        _books = _appDbContext.Books.ToList();

        Book? _book = _books.FirstOrDefault(b => b.IdBook == id);

        if (_book == null) 
            return NotFound();

        Book = _book;

        return Page();
    }
}