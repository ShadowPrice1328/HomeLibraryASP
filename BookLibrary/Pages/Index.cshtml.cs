using BookLibrary.Data;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private List<Book> _books;
        private List<Author> _authors;
        private List<Genre> _genres;
        private readonly AppDbContext _appDbContext;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public List<Book> Books { get; set; }
        public int BooksTotalCount { get; set; }

        public void OnGet()
        {
            _authors = _appDbContext.Authors.ToList();

            _genres = _appDbContext.Genres.ToList();

            _books = _appDbContext.Books.ToList();
            
            Books = _books.Take(5).ToList();
            BooksTotalCount = _books.Count;
        }
    }
}
