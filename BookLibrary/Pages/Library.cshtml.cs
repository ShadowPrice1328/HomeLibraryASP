using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using BookLibrary.Interfaces;

namespace BookLibrary.Pages;

public class LibraryModel : PageModel
{
    private readonly ILogger<LibraryModel> _logger;
    private List<Book> _books;
    private readonly IBookRepository _bookRepository;
    public LibraryModel(ILogger<LibraryModel> logger, IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    public List<Book> Books { get; set; }
    public int BooksTotalCount { get; set; }

    public void OnGet()
    {
        _books = _bookRepository.ReadBooks();
    
        Books = _books;
        BooksTotalCount = _books.Count;
    }
}