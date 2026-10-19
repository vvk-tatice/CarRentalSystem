using System.Text.Json;
using Milestone02_OOP.Models;

namespace Milestone02_OOP.Services;

public class CarRentalService
{
    private readonly string _filePath = "data.json";

    public List<Car> Cars { get; private set; } = new();
    public List<Customer> Customers { get; private set; } = new();
    public List<Rental> Rentals { get; private set; } = new();

    private int _nextCarId = 1;
    private int _nextCustomerId = 1;
    private int _nextRentalId = 1;

    public CarRentalService()
    {
        LoadData();
        
        // Seed default cars if empty
        if (!Cars.Any())
        {
            AddCar("Toyota", "Camry", 50.00m);
            AddCar("Honda", "Civic", 45.00m);
            AddCar("Tesla", "Model 3", 90.00m);
        }
    }

    public Car AddCar(string make, string model, decimal dailyRate)
    {
        var car = new Car(_nextCarId++, make, model, dailyRate);
        Cars.Add(car);
        return car;
    }

    public Customer AddCustomer(string name, string email, string phone)
    {
        var customer = new Customer(_nextCustomerId++, name, email, phone);
        Customers.Add(customer);
        return customer;
    }

    public Rental? CreateRental(int carId, int customerId, int days)
    {
        var car = Cars.FirstOrDefault(c => c.Id == carId && c.IsAvailable);
        var customer = Customers.FirstOrDefault(c => c.Id == customerId);

        if (car == null || customer == null) return null;

        car.IsAvailable = false;
        var rental = new Rental(_nextRentalId++, car.Id, customer.Id, days, car.DailyRate);
        Rentals.Add(rental);
        
        SaveData();
        return rental;
    }

    public void SaveData()
    {
        var data = new
        {
            Cars,
            Customers,
            Rentals,
            NextCarId = _nextCarId,
            NextCustomerId = _nextCustomerId,
            NextRentalId = _nextRentalId
        };

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void LoadData()
    {
        if (!File.Exists(_filePath)) return;

        try
        {
            var json = File.ReadAllText(_filePath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.TryGetProperty("Cars", out var carsElement))
                Cars = JsonSerializer.Deserialize<List<Car>>(carsElement.GetRawText()) ?? new();

            if (root.TryGetProperty("Customers", out var customersElement))
                Customers = JsonSerializer.Deserialize<List<Customer>>(customersElement.GetRawText()) ?? new();

            if (root.TryGetProperty("Rentals", out var rentalsElement))
                Rentals = JsonSerializer.Deserialize<List<Rental>>(rentalsElement.GetRawText()) ?? new();

            if (root.TryGetProperty("NextCarId", out var nc)) _nextCarId = nc.GetInt32();
            if (root.TryGetProperty("NextCustomerId", out var ncust)) _nextCustomerId = ncust.GetInt32();
            if (root.TryGetProperty("NextRentalId", out var nr)) _nextRentalId = nr.GetInt32();
        }
        catch
        {
            // Fallback if JSON format changes
        }
    }
}