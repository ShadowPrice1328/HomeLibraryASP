using BookLibrary.Data;
using BookLibrary.Helpers;
using BookLibrary.Interfaces;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

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
        if (book == null) 
            throw new ArgumentNullException(nameof(book));

        ValidationHelper.ModelValidation(book);

        if (_books != null && _books.Any(b => b.Title == book.Title))
        {
            throw new ArgumentException("This book already exists");
        }

        var existingAuthors = new List<Author>();
        foreach (var author in book.IdAuthors.ToList()) 
        {
            var existingAuthor = _appDbContext.Authors
                .FirstOrDefault(a => a.FirstName == author.FirstName && a.LastName == author.LastName);

            if (existingAuthor != null)
            {
                book.IdAuthors.Remove(author);
                existingAuthors.Add(existingAuthor);
            }
            else { }
        }

        foreach (var existingAuthor in existingAuthors)
        {
            book.IdAuthors.Add(existingAuthor);
        }

        var existingGenres = new List<Genre>();
        foreach (var genre in book.IdGenres.ToList())
        {
            var existingGenre = _appDbContext.Genres
                .FirstOrDefault(g => g.Name == genre.Name);

            if (existingGenre != null)
            {
                book.IdGenres.Remove(genre);
                existingGenres.Add(existingGenre);
            }
        }
        foreach (var existingGenre in existingGenres)
        {
            book.IdGenres.Add(existingGenre);
        }

        _appDbContext.Books.Add(book);
        return _appDbContext.SaveChanges() >= 1;
    }


    public bool DeleteBook(int id)
    {
        var book = _appDbContext.Books.FirstOrDefault(b => b.IdBook == id);
        if (book == null) return false;

        _appDbContext.Books.Remove(book);
        return _appDbContext.SaveChanges() >= 1;
    }

    public Book? ReadBook(int id)
    {
        return _appDbContext.Books
            .Include(b => b.IdAuthors)
            .Include(b => b.IdGenres)
            .FirstOrDefault(b => b.IdBook == id);
    }

    public List<Book> ReadBooks()
    {
        return _appDbContext.Books
            .Include(b => b.IdAuthors)
            .Include(b => b.IdGenres)
            .ToList();
    }

    public bool UpdateBook(Book book)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));

        ValidationHelper.ModelValidation(book);

        var existingBook = _appDbContext.Books
            .Include(b => b.IdAuthors)
            .Include(b => b.IdGenres)
            .FirstOrDefault(b => b.IdBook == book.IdBook);

        if (existingBook == null) return false;

        existingBook.Title = book.Title;
        existingBook.Description = book.Description;
        existingBook.Year = book.Year;
        existingBook.Image = book.Image;
        existingBook.Lent = book.Lent;
        existingBook.Source = book.Source;

        existingBook.IdAuthors.Clear();
        foreach (var author in book.IdAuthors)
        {
            Author? existingAuthor;

            if (author.IdAuthor != 0)
            {
                existingAuthor = _appDbContext.Authors.Find(author.IdAuthor);
            }
            else
            {
                existingAuthor = _appDbContext.Authors
                    .FirstOrDefault(a => a.FirstName == author.FirstName && a.LastName == author.LastName);
            }

            if (existingAuthor == null)
            {
                existingAuthor = new Author
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
                _appDbContext.Authors.Add(existingAuthor);
                _appDbContext.SaveChanges();
            }

            existingBook.IdAuthors.Add(existingAuthor);
        }

        existingBook.IdGenres.Clear();
        foreach (var genre in book.IdGenres)
        {
            Genre? existingGenre;

            if (genre.IdGenre != 0)
            {
                existingGenre = _appDbContext.Genres.Find(genre.IdGenre);
            }
            else
            {
                existingGenre = _appDbContext.Genres
                    .FirstOrDefault(g => g.Name == genre.Name);
            }

            if (existingGenre == null)
            {
                existingGenre = new Genre()
                {
                    Name = genre.Name
                };
                
                _appDbContext.Genres.Add(existingGenre);
                _appDbContext.SaveChanges();
            }

            existingBook.IdGenres.Add(existingGenre);    
        }


        _appDbContext.Books.Update(existingBook);
        return _appDbContext.SaveChanges() >= 1;
    }

}