using System;
using System.Collections.Generic;

namespace BookLibrary.Models;

public partial class Author
{
    public int IdAuthor { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Book> IdBooks { get; set; } = new List<Book>();
}
