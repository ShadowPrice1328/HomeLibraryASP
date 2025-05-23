using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookLibrary.CustomValidators;

namespace BookLibrary.Models;

public partial class Book
{
    public int IdBook { get; set; }

    [Required(ErrorMessage = "{0} can't be blank")]
    [MaximumYearValidator]
    public DateOnly Year { get; set; }

    [Required(ErrorMessage = "{0} can't be blank")]
    [StringLength(1000, MinimumLength = 0, ErrorMessage = "{0} must be between {2} and {1} characters long.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "{0} can't be blank")]
    [StringLength(255, ErrorMessage = "{0} can't exceed {1} characters.")]
    public string Title { get; set; } = null!;

    [RegularExpression(@".*\.(jpg|jpeg|png)$", ErrorMessage = "Only image files are allowed (jpg, jpeg, png).")]
    public string Image { get; set; } = null!;

    [Required(ErrorMessage = "{0} can't be blank")]
    [StringLength(50, ErrorMessage = "{0} can't exceed {1} characters.")]
    public string Source { get; set; } = null!;

    public bool Lent { get; set; }

    public virtual ICollection<Author> IdAuthors { get; set; } = new List<Author>();

    public virtual ICollection<Genre> IdGenres { get; set; } = new List<Genre>();
}
