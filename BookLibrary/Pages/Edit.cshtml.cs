using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Data;
using BookLibrary.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Pages;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private readonly IBookRepository _bookRepository;

    [BindProperty]
    public string Authors { get; set; } = string.Empty;

    [BindProperty]
    public string Genres { get; set; } = string.Empty;

    [BindProperty]
    [Range(0, int.MaxValue, ErrorMessage = "{0} must be a valid year")]
    public int YearInput { get; set; }

    [BindProperty]
    public int IdBook { get; set; }
    
    public EditModel(ILogger<EditModel> logger, IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    [BindProperty]
    public Book Book { get; set; }
    public IActionResult OnGet(int id)
    {
        Book? _book = _bookRepository.ReadBook(id);

        if (_book == null)
            return NotFound();

        Book = _book;

        return Page();
    }

    public IActionResult OnPost()
    {
        try
        {
            Book.Year = new DateOnly(YearInput, 1, 1);
        }
        catch
        {
            ModelState.AddModelError("YearInput", "Invalid year value.");
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (!string.IsNullOrWhiteSpace(Authors))
        {
            var authors = Authors.Split(',')
                .Select(a => a.Trim())
                .Where(a => !string.IsNullOrEmpty(a))
                .Distinct();

            foreach (var authorFullName in authors)
            {
                var names = authorFullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (names.Length >= 2)
                {
                    Author author = new()
                    {
                        FirstName = names[0],
                        LastName = string.Join(' ', names.Skip(1))
                    };

                    Book.IdAuthors.Add(author);
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(Genres))
        {
            var genres = Genres.Split(',')
                .Select(g => g.Trim())
                .Where(g => !string.IsNullOrEmpty(g))
                .Distinct();

            foreach (var genreName in genres)
            {
                Genre genre = new() { Name = genreName };
                Book.IdGenres.Add(genre);
            }
        }

        Book.IdBook = IdBook;

        try
        {
            if (_bookRepository.UpdateBook(Book))
            {
                return RedirectToPage("/Library");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating the book");
            ModelState.AddModelError(string.Empty, "An error occurred while updating the book.");
        }

        return Page();
    }
}