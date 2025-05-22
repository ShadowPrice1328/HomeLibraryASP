using BookLibrary.Data;
using BookLibrary.Helpers;
using BookLibrary.Interfaces;
using BookLibrary.Models;

namespace BookLibrary.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly List<Book> _books;
    public BookRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        _books = _appDbContext.Books.ToList();
    }
    public bool CreateBook(Book book)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));

        ValidationHelper.ModelValidation(book);

        if (_books != null && _books.Where(b => b.Title == book.Title).Any())
        {
            throw new ArgumentException("This book already exists");
        }

        _appDbContext.Books.Add(book);
        return _appDbContext.SaveChanges() >= 1;
    }

    public bool DeleteBook(int id)
    {
        throw new NotImplementedException();
    }

    public Book ReadBook(int id)
    {
        throw new NotImplementedException();
    }

    public List<Book> ReadBooks()
    {
        throw new NotImplementedException();
    }

    public bool UpdateBook(Book book)
    {
        throw new NotImplementedException();
    }
}