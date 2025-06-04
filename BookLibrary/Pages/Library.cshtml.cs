using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using BookLibrary.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult OnPostDelete(int id)
    {
        var book = _bookRepository.ReadBook(id);
        if (book == null)
        {
            return NotFound();
        }

        _bookRepository.DeleteBook(id);
        return RedirectToPage("/Library");
    }

    public void OnGet(string? sortBy, string? query)
    {
        _books = _bookRepository.ReadBooks();

        if (!string.IsNullOrEmpty(query))
        {
            _books = _books.Where(b => !string.IsNullOrEmpty(b.Title) && b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "title":
                    _books = _books.OrderBy(b => b.Title).ToList();
                    break;
                case "year":
                    _books = _books.OrderBy(b => b.Year).ToList();
                    break;
                case "author":
                    _books = _books
                        .OrderBy(b => b.IdAuthors.FirstOrDefault()?.LastName ?? "")
                        .ThenBy(b => b.IdAuthors.FirstOrDefault()?.FirstName ?? "")
                        .ToList();
                    break;
            }
        }

        Books = _books;
        BooksTotalCount = _books.Count;
    }

}