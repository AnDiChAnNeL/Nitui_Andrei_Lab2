using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Models;
using Nitui_Andrei_Lab2.Data;

namespace Nitui_Andrei_Lab2.Pages.Borrowings;

public class DetailsModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public DetailsModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
        if (borrowing is null)
        {
            return NotFound();
        }
        else
        {
            Borrowing = borrowing;
        }

        return Page();
    }
}
