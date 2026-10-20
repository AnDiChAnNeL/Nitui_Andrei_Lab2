using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Publishers;

public class IndexModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public IndexModel(Nitui_Andrei_Lab2Context context) { _context = context; }
    public IList<Publisher> Publishers { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Publishers = await _context.Publisher.ToListAsync();
    }
}
