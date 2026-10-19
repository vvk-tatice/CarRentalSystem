using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Milestone01_BasicConsole
{
    // Simple data structures for Milestone 1
    public class CarRentalItem
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CarModel { get; set; } = string.Empty;
        public int Days { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalPrice => Days * DailyRate;
    }

    internal class Program
    {
        private static readonly string FilePath = "rentals.json";
        private static List<CarRentalItem> rentals = new List<CarRentalItem>();
        private static int nextId = 1;

        static void Main(string[] args)
        {
            LoadDataFromJson();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    CAR RENTAL SYSTEM - MILESTONE 01    ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Add New Rental");
                Console.WriteLine("2. List All Rentals");
                Console.WriteLine("3. Find Rental by Customer Name");
                Console.WriteLine("4. Delete/Cancel Rental");
                Console.WriteLine("5. Save Data");
                Console.WriteLine("6. Exit");
                Console.WriteLine("========================================");
                Console.Write("Select an option (1-6): ");

                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddRental();
                        break;
                    case "2":
                        ListRentals();
                        break;
                    case "3":
                        FindRental();
                        break;
                    case "4":
                        DeleteRental();
                        break;
                    case "5":
                        SaveDataToJson();
                        Console.WriteLine("Data saved! Press Enter to continue.");
                        Console.ReadLine();
                        break;
                    case "6":
                        SaveDataToJson();
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to retry.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void AddRental()
        {
            Console.Clear();
            Console.WriteLine("--- Add New Rental ---");

            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine() ?? "Unknown";

            Console.Write("Enter Car Model (e.g. Toyota Camry): ");
            string car = Console.ReadLine() ?? "Generic Car";

            int days = ReadIntInput("Enter Rental Days: ");
            decimal rate = ReadDecimalInput("Enter Daily Rate ($): ");

            var newRental = new CarRentalItem
            {
                Id = nextId++,
                CustomerName = name,
                CarModel = car,
                Days = days,
                DailyRate = rate
            };

            rentals.Add(newRental);
            SaveDataToJson();

            Console.WriteLine($"\nRental added successfully! Total Cost: ${newRental.TotalPrice:F2}");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void ListRentals()
        {
            Console.Clear();
            Console.WriteLine("--- List of Active Rentals ---");

            if (rentals.Count == 0)
            {
                Console.WriteLine("No rentals found.");
            }
            else
            {
                foreach (var r in rentals)
                {
                    Console.WriteLine($"[ID: {r.Id}] {r.CustomerName} | Car: {r.CarModel} | Days: {r.Days} | Rate: ${r.DailyRate}/day | Total: ${r.TotalPrice:F2}");
                }
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void FindRental()
        {
            Console.Clear();
            Console.WriteLine("--- Find Rental ---");
            Console.Write("Enter Customer Name to Search: ");
            string query = (Console.ReadLine() ?? "").ToLower();

            var matches = rentals.FindAll(r => r.CustomerName.ToLower().Contains(query));

            if (matches.Count == 0)
            {
                Console.WriteLine("No matching rentals found.");
            }
            else
            {
                Console.WriteLine($"\nFound {matches.Count} match(es):");
                foreach (var r in matches)
                {
                    Console.WriteLine($"[ID: {r.Id}] {r.CustomerName} rented {r.CarModel} for {r.Days} days (${r.TotalPrice:F2})");
                }
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void DeleteRental()
        {
            Console.Clear();
            Console.WriteLine("--- Cancel/Delete Rental ---");
            int id = ReadIntInput("Enter Rental ID to delete: ");

            var item = rentals.Find(r => r.Id == id);
            if (item != null)
            {
                rentals.Remove(item);
                SaveDataToJson();
                Console.WriteLine($"Rental ID {id} deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Rental ID {id} not found.");
            }

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        private static void SaveDataToJson()
        {
            try
            {
                string json = JsonSerializer.Serialize(rentals, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        private static void LoadDataFromJson()
        {
            if (!File.Exists(FilePath)) return;

            try
            {
                string json = File.ReadAllText(FilePath);
                var loaded = JsonSerializer.Deserialize<List<CarRentalItem>>(json);
                if (loaded != null)
                {
                    rentals = loaded;
                    int maxId = 0;
                    foreach (var item in rentals)
                    {
                        if (item.Id > maxId) maxId = item.Id;
                    }
                    nextId = maxId + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private static int ReadIntInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result > 0)
                    return result;
                Console.WriteLine("Invalid number. Please enter a positive integer.");
            }
        }

        private static decimal ReadDecimalInput(string prompt)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out result) && result > 0)
                    return result;
                Console.WriteLine("Invalid amount. Please enter a positive number.");
            }
        }
    }
}