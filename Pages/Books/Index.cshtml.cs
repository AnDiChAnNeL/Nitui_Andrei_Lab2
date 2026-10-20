
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Books;

public class IndexModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public IndexModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = new List<Book>();

    public BookData BookD { get; set; } = new BookData();

    public int BookID { get; set; }
    public int CategoryID { get; set; }

    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID)
    {
        ViewData["AuthorID"] = new SelectList(
            _context.Authors, "ID", "LastName");

        var books = _context.Book
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .AsQueryable();

        if (AuthorID != null)
        {
            books = books.Where(b => b.AuthorID == AuthorID);
        }

        Book = await books
            .OrderBy(b => b.Title)
            .ToListAsync();

        BookD.Books = Book;

        if (id != null)
        {
            BookID = id.Value;

            var selectedBook = Book
                .FirstOrDefault(b => b.ID == id.Value);

            if (selectedBook != null)
            {
                BookD.Categories = selectedBook.BookCategories?
                    .Select(bc => bc.Category)
                    .ToList() ?? new List<Category>();
            }
        }
    }
}
