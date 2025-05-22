using BookLibrary.Interfaces;
using BookLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private List<Book> _books;
        private readonly IBookRepository _bookRepository;

        public IndexModel(ILogger<IndexModel> logger, IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public List<Book> Books { get; set; }
        public int BooksTotalCount { get; set; }

        public void OnGet()
        {
            _books = _bookRepository.ReadBooks();
            
            Books = _books.Take(5).ToList();
            BooksTotalCount = Books.Count;
        }
    }
}
