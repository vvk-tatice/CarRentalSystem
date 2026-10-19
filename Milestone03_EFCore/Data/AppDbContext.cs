using Microsoft.EntityFrameworkCore;
using Milestone03_EFCore.Models;

namespace Milestone03_EFCore.Data;

public class AppDbContext : DbContext
{
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // This creates a local SQLite database file named "carrental.db" in your project folder
        optionsBuilder.UseSqlite("Data Source=carrental.db");
    }
}