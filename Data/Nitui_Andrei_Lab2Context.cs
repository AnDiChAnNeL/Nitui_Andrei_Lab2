using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Models;

namespace Nitui_Andrei_Lab2.Data
{
    public class Nitui_Andrei_Lab2Context : DbContext
    {
        public Nitui_Andrei_Lab2Context(DbContextOptions<Nitui_Andrei_Lab2Context> options) : base(options) { }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<Authors> Authors { get; set; } = default!;
    }
}
