using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;
using BookLibrary.Interfaces;

namespace BookLibrary.Pages;

public class DetailsModel : PageModel
{
    private readonly ILogger<DetailsModel> _logger;
    private List<Book> _books;
    private readonly AppDbContext _appDbContext;
    private readonly IBookRepository _bookRepository;
    public DetailsModel(ILogger<DetailsModel> logger, IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        _logger = logger;
    }

    public Book Book { get; set; }
    public IActionResult OnGet(int id)
    {
        Book? _book = _bookRepository.ReadBook(id);

        if (_book == null) 
            return NotFound();

        Book = _book;

        return Page();
    }
}