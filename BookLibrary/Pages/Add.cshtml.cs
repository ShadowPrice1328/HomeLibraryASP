using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using BookLibrary.Interfaces;

namespace BookLibrary.Pages;

public class AddModel : PageModel
{
    private readonly ILogger<AddModel> _logger;
    private readonly IBookRepository _bookRepository;
    public AddModel(ILogger<AddModel> logger, IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
}