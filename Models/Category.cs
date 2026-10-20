namespace Nitui_Andrei_Lab2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<BookCategory>? BookCatgeories { get; set; }
    }
}
