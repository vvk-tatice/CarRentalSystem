using Milestone02_OOP.Services;

var service = new CarRentalService();

while (true)
{
    Console.WriteLine("\n=================================");
    Console.WriteLine(" CAR RENTAL SYSTEM - MILESTONE 02");
    Console.WriteLine("=================================");
    Console.WriteLine("1. View Available Cars");
    Console.WriteLine("2. Register Customer");
    Console.WriteLine("3. Rent a Car");
    Console.WriteLine("4. List All Rentals");
    Console.WriteLine("5. Exit");
    Console.Write("Select an option (1-5): ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.WriteLine("\n--- Available Cars ---");
            foreach (var car in service.Cars.Where(c => c.IsAvailable))
            {
                Console.WriteLine($"[{car.Id}] {car.Make} {car.Model} - ${car.DailyRate}/day");
            }
            break;

        case "2":
            Console.Write("Enter Full Name: ");
            var name = Console.ReadLine() ?? "";
            Console.Write("Enter Email: ");
            var email = Console.ReadLine() ?? "";
            Console.Write("Enter Phone Number: ");
            var phone = Console.ReadLine() ?? "";

            var customer = service.AddCustomer(name, email, phone);
            Console.WriteLine($"Customer registered successfully! Customer ID: {customer.Id}");
            break;

        case "3":
            Console.Write("Enter Car ID to Rent: ");
            if (!int.TryParse(Console.ReadLine(), out int carId)) break;

            Console.Write("Enter Customer ID: ");
            if (!int.TryParse(Console.ReadLine(), out int custId)) break;

            Console.Write("Enter Number of Rental Days: ");
            if (!int.TryParse(Console.ReadLine(), out int days)) break;

            var rental = service.CreateRental(carId, custId, days);
            if (rental != null)
            {
                Console.WriteLine($"\nSuccess! Rental ID {rental.Id} created. Total Price: ${rental.TotalPrice}");
            }
            else
            {
                Console.WriteLine("\nError: Invalid Car ID, Customer ID, or car is already rented.");
            }
            break;

        case "4":
            Console.WriteLine("\n--- Active Rentals ---");
            if (!service.Rentals.Any())
            {
                Console.WriteLine("No active rentals found.");
            }
            else
            {
                foreach (var r in service.Rentals)
                {
                    Console.WriteLine($"Rental ID: {r.Id} | Car ID: {r.CarId} | Customer ID: {r.CustomerId} | Days: {r.RentalDays} | Total: ${r.TotalPrice}");
                }
            }
            break;

        case "5":
            service.SaveData();
            Console.WriteLine("Data saved. Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}