using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Pages;

public class AddModel : PageModel
{
    private readonly ILogger<AddModel> _logger;
    private readonly IBookRepository _bookRepository;
    private readonly IWebHostEnvironment _environment;

    [BindProperty]
    public Book Book { get; set; } = new();

    [BindProperty]
    public string Authors { get; set; } = string.Empty;

    [BindProperty]
    public string Genres { get; set; } = string.Empty;

    [BindProperty]
    [Range(0, int.MaxValue, ErrorMessage = "{0} must be a valid year")]
    public int YearInput { get; set; }

    public AddModel(ILogger<AddModel> logger, IBookRepository bookRepository, IWebHostEnvironment environment)
    {
        _logger = logger;
        _bookRepository = bookRepository;
        _environment = environment;
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

        try
        {
            if (_bookRepository.CreateBook(Book))
            {
                return RedirectToPage("/Library");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating book");
            ModelState.AddModelError(string.Empty, "An error occurred while saving the book.");
        }

        return Page();
    }

    public IActionResult OnPostUploadCover(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return new JsonResult(new { success = false, message = "No file uploaded." });
        }

        try
        {
            // Ensure directory exists
            var uploadsDir = Path.Combine(_environment.WebRootPath, "assets", "Images");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            // Save file
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var relativePath = $"{fileName}";
            return new JsonResult(new { success = true, filePath = relativePath });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = "An error occurred while uploading the file.", error = ex.Message });
        }
    }

    public IActionResult OnGet()
    {
        return Page();
    }
}