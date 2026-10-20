using AuthorEntity = Nitui_Andrei_Lab2.Models.Authors;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Authors;

public class IndexModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public IndexModel(Nitui_Andrei_Lab2Context context) { _context = context; }
    public IList<AuthorEntity> Authors { get; set; } = default!;
    public async Task OnGetAsync()
    {
        Authors = await _context.Authors.ToListAsync();
    }
}
