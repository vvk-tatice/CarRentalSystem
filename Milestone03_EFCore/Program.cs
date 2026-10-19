using Milestone03_EFCore.Services;

var service = new CarRentalService();

while (true)
{
    Console.WriteLine("\n=================================");
    Console.WriteLine(" CAR RENTAL SYSTEM - MILESTONE 03");
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
            foreach (var car in service.GetAvailableCars())
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
            if (!int.TryParse(Console.ReadLine(), out int customerId)) break;

            Console.Write("Enter Number of Rental Days: ");
            if (!int.TryParse(Console.ReadLine(), out int days)) break;

            var rental = service.CreateRental(carId, customerId, days);
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
            var rentals = service.GetAllRentals();
            if (!rentals.Any())
            {
                Console.WriteLine("No active rentals found.");
            }
            else
            {
                foreach (var r in rentals)
                {
                    Console.WriteLine($"Rental ID: {r.Id} | Car ID: {r.CarId} | Customer ID: {r.CustomerId} | Days: {r.RentalDays} | Total: ${r.TotalPrice}");
                }
            }
            break;

        case "5":
            Console.WriteLine("Exiting application. Database changes are saved automatically!");
            return;

        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}