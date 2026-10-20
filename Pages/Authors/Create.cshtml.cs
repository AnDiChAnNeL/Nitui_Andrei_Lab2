using AuthorEntity = Nitui_Andrei_Lab2.Models.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;
namespace Nitui_Andrei_Lab2.Pages.Authors;

public class CreateModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public CreateModel(Nitui_Andrei_Lab2Context context) { _context = context; }
    [BindProperty]
    public AuthorEntity Author { get; set; } = default!;

    public IActionResult OnGet() { return Page(); }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _context.Authors.Add(Author);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
