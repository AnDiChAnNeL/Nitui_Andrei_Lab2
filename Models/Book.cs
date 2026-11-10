using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nitui_Andrei_Lab2.Models;

public class Book
{
    public int ID { get; set; }

    [Display(Name = "Book Title")]
    [Required(ErrorMessage = "Titlul este obligatoriu.")]
    [StringLength(150, MinimumLength = 3,
        ErrorMessage = "Titlul trebuie să aibă între 3 și 150 de caractere.")]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "decimal(6, 2)")]
    [Range(0.01, 500, ErrorMessage = "Prețul trebuie să fie între 0,01 și 500.")]
    public decimal Price { get; set; }

    [DataType(DataType.Date)]
    public DateTime PublishingDate { get; set; }

    public int? PublisherID { get; set; }
    public Publisher? Publisher { get; set; }

    public int? AuthorID { get; set; }
    public Authors? Author { get; set; }

    public ICollection<BookCategory>? BookCategories { get; set; }
    public ICollection<Borrowing>? Borrowings { get; set; }
}