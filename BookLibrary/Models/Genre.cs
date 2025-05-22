using System;
using System.Collections.Generic;

namespace BookLibrary.Models;

public partial class Genre
{
    public int IdGenre { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> IdBooks { get; set; } = new List<Book>();
}
