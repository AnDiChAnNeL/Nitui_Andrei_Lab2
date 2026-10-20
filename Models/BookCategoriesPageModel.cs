
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nitui_Andrei_Lab2.Data;

namespace Nitui_Andrei_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList =
            new List<AssignedCategoryData>();

        public void PopulateAssignedCategoryData(
            Nitui_Andrei_Lab2Context context, Book book)
        {
            var allCategories = context.Category;

            var bookCategories = new HashSet<int>(
                book.BookCategories?.Select(c => c.CategoryID)
                ?? Enumerable.Empty<int>());

            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.Id,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.Id)
                });
            }
        }

        public void UpdateBookCategories(
            Nitui_Andrei_Lab2Context context,
            string[] selectedCategories,
            Book bookToUpdate)
        {
            bookToUpdate.BookCategories ??= new List<BookCategory>();

            var selectedCategoriesHS = new HashSet<string>(
                selectedCategories ?? Array.Empty<string>());

            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(c => c.CategoryID));

            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.Id.ToString()))
                {
                    if (!bookCategories.Contains(cat.Id))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.Id
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.Id))
                    {
                        var bookToRemove = bookToUpdate.BookCategories
                            .Single(c => c.CategoryID == cat.Id);

                        context.Remove(bookToRemove);
                    }
                }
            }
        }


    }
}
