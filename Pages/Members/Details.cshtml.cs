using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Models;
using Nitui_Andrei_Lab2.Data;

namespace Nitui_Andrei_Lab2.Pages.Members;

public class DetailsModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public DetailsModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public Member Member { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Member.FirstOrDefaultAsync(m => m.ID == id);
        if (member is null)
        {
            return NotFound();
        }
        else
        {
            Member = member;
        }

        return Page();
    }
}
