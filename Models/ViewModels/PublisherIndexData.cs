using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } = default!;
        public IEnumerable<Book>? Books { get; set; }
    }
}