using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Books;

public class EditModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public EditModel(Nitui_Andrei_Lab2Context context) { _context = context; }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var book = await _context.Book.FirstOrDefaultAsync(b => b.ID == id);
        if (book == null) return NotFound();
        Book = book;
        FillLists();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            FillLists();
            return Page();
        }
        _context.Attach(Book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }

    private void FillLists()
    {
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName");
    }
}
