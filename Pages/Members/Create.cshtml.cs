using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Models;
using Nitui_Andrei_Lab2.Data;

namespace Nitui_Andrei_Lab2.Pages.Members;

public class CreateModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public CreateModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Member Member { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Member.Add(Member);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
