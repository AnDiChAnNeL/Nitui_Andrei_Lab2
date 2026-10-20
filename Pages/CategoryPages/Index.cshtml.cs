using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Models;
using Nitui_Andrei_Lab2.Data;

namespace Nitui_Andrei_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public IndexModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
