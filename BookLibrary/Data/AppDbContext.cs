using System;
using System.Collections.Generic;
using BookLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception)
        {
            return;
        }
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.IdAuthor).HasName("PK__Authors__83F33C8BE57BC1D3");

            entity.Property(e => e.IdAuthor).HasColumnName("ID_Author");
            entity.Property(e => e.FirstName)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdBook).HasName("PK__Books__DE8DF038131185EC");

            entity.Property(e => e.IdBook).HasColumnName("ID_Book");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Image)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(128)
                .IsUnicode(false);

            entity.HasMany(d => d.IdAuthors).WithMany(p => p.IdBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("IdAuthor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_Books_Authors__Authors"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_Books_Authors__Books"),
                    j =>
                    {
                        j.HasKey("IdBook", "IdAuthor").HasName("PK__Books_Au__76B2C3F0211F3A8B");
                        j.ToTable("Books_Authors");
                        j.IndexerProperty<int>("IdBook").HasColumnName("ID_Book");
                        j.IndexerProperty<int>("IdAuthor").HasColumnName("ID_Author");
                    });

            entity.HasMany(d => d.IdGenres).WithMany(p => p.IdBooks)
                .UsingEntity<Dictionary<string, object>>(
                    "BooksGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("IdGenre")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_Books_Genres__Genres"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("IdBook")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_Books_Genres__Books"),
                    j =>
                    {
                        j.HasKey("IdBook", "IdGenre").HasName("PK__Books_Ge__793EEABB2917F912");
                        j.ToTable("Books_Genres");
                        j.IndexerProperty<int>("IdBook").HasColumnName("ID_Book");
                        j.IndexerProperty<int>("IdGenre").HasColumnName("ID_Genre");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("PK__Genres__7B31A83B8DE42542");

            entity.Property(e => e.IdGenre).HasColumnName("ID_Genre");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Author>().HasData(
            new Author { IdAuthor = 1, FirstName = "George", LastName = "Orwell" },
            new Author { IdAuthor = 2, FirstName = "Aldous", LastName = "Huxley" }
        );

        modelBuilder.Entity<Genre>().HasData(
            new Genre { IdGenre = 1, Name = "Novel" },
            new Genre { IdGenre = 2, Name = "Dystopian" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                IdBook = 1,
                Title = "1984",
                Description = "A dystopian novel by George Orwell.",
                Year = DateOnly.FromDateTime(new DateTime(1949, 1, 1)),
                Image = "1984.jpg",
                Lent = false,
                Source = "Purchased"
            },
            new Book
            {
                IdBook = 2,
                Title = "Brave New World",
                Description = "A dystopian novel by Aldous Huxley.",
                Year = DateOnly.FromDateTime(new DateTime(1932, 1, 1)),
                Image = "BraveNewWorld.jpg",
                Lent = false,
                Source = "Gifted"
            }
        );
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
