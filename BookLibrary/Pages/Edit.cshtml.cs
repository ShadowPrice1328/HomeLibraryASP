using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;
using BookLibrary.Interfaces;

namespace BookLibrary.Pages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private List<Book> _books;
    private List<Author> _authors;
    private List<Genre> _genres;
    private readonly IBookRepository _bookRepository;

    public EditModel(ILogger<EditModel> logger, IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
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