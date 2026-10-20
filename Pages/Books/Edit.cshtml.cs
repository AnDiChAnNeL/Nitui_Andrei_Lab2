using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Books;

public class EditModel : BookCategoriesPageModel
{
    private readonly Nitui_Andrei_Lab2Context _context;
    public EditModel(Nitui_Andrei_Lab2Context context) { _context = context; }

    [BindProperty]
    public Book Book { get; set; } = default!;


    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var book = await _context.Book
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (book == null)
            return NotFound();

        Book = book;

        PopulateAssignedCategoryData(_context, Book);

        FillLists();

        return Page();
    }



    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var bookToUpdate = await _context.Book
            .Include(b => b.BookCategories)
            .ThenInclude(bc => bc.Category)
            .FirstOrDefaultAsync(b => b.ID == Book.ID);

        if (bookToUpdate == null)
            return NotFound();

        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            b => b.Title,
            b => b.Price,
            b => b.PublishingDate,
            b => b.AuthorID,
            b => b.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        Book = bookToUpdate;
        PopulateAssignedCategoryData(_context, bookToUpdate);
        FillLists();

        return Page();
    }


    private void FillLists()
    {
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
        ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName");
    }
}
