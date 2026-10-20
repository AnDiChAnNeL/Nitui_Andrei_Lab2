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
    public IndexModel(Nitui_Andrei_Lab2Context context) { _context = context; }

    public IList<Book> Book { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int? AuthorID { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName");
        var books = _context.Book.Include(b => b.Author).Include(b => b.Publisher).AsQueryable();
        if (AuthorID != null)
            books = books.Where(b => b.AuthorID == AuthorID);
        Book = await books.ToListAsync();
    }
}
