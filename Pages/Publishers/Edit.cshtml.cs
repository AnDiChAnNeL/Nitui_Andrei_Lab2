using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;
namespace Nitui_Andrei_Lab2.Pages.Publishers;

public class EditModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public EditModel(Nitui_Andrei_Lab2Context context) { _context = context; }
    [BindProperty]
    public Publisher Publisher { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();
        var item = await _context.Publisher.FindAsync(id);
        if (item == null) return NotFound();
        Publisher = item;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        _context.Attach(Publisher).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
