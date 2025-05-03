using Microsoft.AspNetCore.Mvc;
using BookLibrary.Models;

namespace BookLibrary.Components
{
    public class BookPreviewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Book item)
        {
            return View(item);
        }
    }
}