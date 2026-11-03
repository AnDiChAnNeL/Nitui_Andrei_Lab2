
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Nitui_Andrei_Lab2Context _context;

        public EditModel(Nitui_Andrei_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing
                .FirstOrDefaultAsync(b => b.ID == id);

            if (borrowing == null)
            {
                return NotFound();
            }

            Borrowing = borrowing;

            LoadSelectLists();

            return Page();
        }

        private void LoadSelectLists()
        {
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
                {
                    x.ID,
                    BookFullName = x.Title + " - " +
                        (x.Author != null
                            ? x.Author.LastName + " " +
                              x.Author.FirstName
                            : "")
                })
                .ToList();

            ViewData["BookID"] = new SelectList(
                bookList,
                "ID",
                "BookFullName",
                Borrowing.BookID
            );

            var memberList = _context.Member
                .ToList()
                .Select(m => new
                {
                    m.ID,
                    MemberFullName =
                        string.IsNullOrWhiteSpace(m.FullName)
                            ? m.Email
                            : m.FullName
                });

            ViewData["MemberID"] = new SelectList(
                memberList,
                "ID",
                "MemberFullName",
                Borrowing.MemberID
            );
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadSelectLists();
                return Page();
            }

            var borrowing = await _context.Borrowing
                .FindAsync(Borrowing.ID);

            if (borrowing == null)
            {
                return NotFound();
            }

            borrowing.MemberID = Borrowing.MemberID;
            borrowing.BookID = Borrowing.BookID;
            borrowing.ReturnDate = Borrowing.ReturnDate;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
