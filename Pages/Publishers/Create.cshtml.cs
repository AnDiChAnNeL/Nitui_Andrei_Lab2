using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;
namespace Nitui_Andrei_Lab2.Pages.Publishers;

public class CreateModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public CreateModel(Nitui_Andrei_Lab2Context context) { _context = context; }
    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    public IActionResult OnGet() { return Page(); }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _context.Publisher.Add(Publisher);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
