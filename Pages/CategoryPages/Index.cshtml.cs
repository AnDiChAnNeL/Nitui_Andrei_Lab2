using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;
using Nitui_Andrei_Lab2.ViewModels;

namespace Nitui_Andrei_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public IndexModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public CategoryIndexData CategoryData { get; set; } = default!;
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();
        CategoryData.Categories = await _context.Category
            .Include(c => c.BookCatgeories)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category category = CategoryData.Categories
                .Where(c => c.Id == id.Value).Single();
            CategoryData.Books = category.BookCatgeories!
                .Select(bc => bc.Book);
        }
    }
}