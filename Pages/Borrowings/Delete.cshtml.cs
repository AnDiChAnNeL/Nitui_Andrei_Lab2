
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Borrowings;

public class DeleteModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public DeleteModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .Include(b => b.Member)
            .Include(b => b.Book)
                .ThenInclude(b => b!.Author)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (borrowing == null)
        {
            return NotFound();
        }

        Borrowing = borrowing;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        var borrowingId = id ?? Borrowing?.ID;

        if (borrowingId == null || borrowingId == 0)
        {
            return NotFound();
        }

        var borrowing = await _context.Borrowing
            .FindAsync(borrowingId.Value);

        if (borrowing != null)
        {
            _context.Borrowing.Remove(borrowing);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
