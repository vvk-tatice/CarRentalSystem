using Milestone03_EFCore.Data;
using Milestone03_EFCore.Models;

namespace Milestone03_EFCore.Services;

public class CarRentalService
{
    private readonly AppDbContext _context = new();

    public CarRentalService()
    {
        // Ensure the database is created
        _context.Database.EnsureCreated();

        // Seed default cars if table is empty
        if (!_context.Cars.Any())
        {
            _context.Cars.AddRange(
                new Car { Make = "Toyota", Model = "Camry", DailyRate = 50.00m, IsAvailable = true },
                new Car { Make = "Honda", Model = "Civic", DailyRate = 45.00m, IsAvailable = true },
                new Car { Make = "Tesla", Model = "Model 3", DailyRate = 90.00m, IsAvailable = true }
            );
            _context.SaveChanges();
        }
    }

    public List<Car> GetAvailableCars() => _context.Cars.Where(c => c.IsAvailable).ToList();

    public Customer AddCustomer(string name, string email, string phone)
    {
        var customer = new Customer { FullName = name, Email = email, PhoneNumber = phone };
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public Rental? CreateRental(int carId, int customerId, int days)
    {
        var car = _context.Cars.FirstOrDefault(c => c.Id == carId && c.IsAvailable);
        var customer = _context.Customers.FirstOrDefault(c => c.Id == customerId);

        if (car == null || customer == null) return null;

        car.IsAvailable = false;
        
        var rental = new Rental
        {
            CarId = car.Id,
            CustomerId = customer.Id,
            StartDate = DateTime.Now,
            RentalDays = days,
            TotalPrice = car.DailyRate * days
        };

        _context.Rentals.Add(rental);
        _context.SaveChanges();
        return rental;
    }

    public List<Rental> GetAllRentals() => _context.Rentals.ToList();
}