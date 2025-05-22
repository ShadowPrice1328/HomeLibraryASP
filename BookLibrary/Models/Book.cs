using System;
using System.Collections.Generic;

namespace BookLibrary.Models;

public partial class Book
{
    public int IdBook { get; set; }

    public DateOnly Year { get; set; }

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Image { get; set; } = null!;

    public string Source { get; set; } = null!;

    public bool Lent { get; set; }

    public virtual ICollection<Author> IdAuthors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> IdGenres { get; set; } = new List<Genre>();
}
