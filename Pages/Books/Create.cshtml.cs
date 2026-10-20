using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Books;

public class CreateModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public CreateModel(Nitui_Andrei_Lab2Context context) { _context = context; }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return OnGet();
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
