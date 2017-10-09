using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }

        // Part 1 - Setting Up the New Model
        public DbSet<CheeseCategory> Categories { get; set; } // By naming this property Categories, EF will create a table within our database of the same name.

        public CheeseDbContext(DbContextOptions<CheeseDbContext> options) 
            : base(options)
        { }

    }
}
