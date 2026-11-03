
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Borrowings
{
    public class IndexModel : PageModel
    {
        private readonly Nitui_Andrei_Lab2Context _context;

        public IndexModel(Nitui_Andrei_Lab2Context context)
        {
            _context = context;
        }

        public IList<Borrowing> Borrowing { get; set; } = new List<Borrowing>();

        public async Task OnGetAsync()
        {
            Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                    .ThenInclude(b => b!.Author)
                .Include(b => b.Member)
                .ToListAsync();
        }
    }
}
