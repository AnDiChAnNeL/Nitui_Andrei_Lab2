using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Books;

[Authorize(Roles = "Admin")]
public class CreateModel : BookCategoriesPageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;

    public CreateModel(Nitui_Andrei_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public IActionResult OnGet()
    {
        FillLists();
        PopulateAssignedCategoryData(_context, new Book());
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string[]? selectedCategories)
    {
        Book.BookCategories = new List<BookCategory>();
        UpdateBookCategories(_context, selectedCategories ?? Array.Empty<string>(), Book);

        if (!ModelState.IsValid)
        {
            FillLists();
            PopulateAssignedCategoryData(_context, Book);
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private void FillLists()
    {
        ViewData["PublisherID"] = new SelectList(
            _context.Publisher, "ID", "PublisherName");

        ViewData["AuthorID"] = new SelectList(
            _context.Authors, "ID", "LastName");
    }
}