using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Members;

public class EditModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public EditModel(Nitui_Andrei_Lab2Context context)
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

        Member = member;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync([FromForm(Name = "Member.ID")] int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var member = await _context.Member.FindAsync(id.Value);
        if (member is null)
        {
            return NotFound();
        }

        Member = member;

        if (!await TryUpdateModelAsync(
            Member,
            "Member",
            m => m.FirstName,
            m => m.LastName,
            m => m.Adress,
            m => m.Phone))
        {
            return Page();
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!MemberExists(Member.ID))
        {
            return NotFound();
        }

        return RedirectToPage("./Index");
    }

    private bool MemberExists(int id)
    {
        return _context.Member.Any(member => member.ID == id);
    }
}