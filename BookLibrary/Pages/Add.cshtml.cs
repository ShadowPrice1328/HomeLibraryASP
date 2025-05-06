using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Pages;

public class AddModel : PageModel
{
    private readonly ILogger<AddModel> _logger;
    public AddModel(ILogger<AddModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        return Page();
    }
}