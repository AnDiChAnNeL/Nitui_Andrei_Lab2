using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.ViewModels
{
    public class CategoryIndexData
    {
        public IEnumerable<Category> Categories { get; set; } = default!;
        public IEnumerable<Book>? Books { get; set; }
    }
}