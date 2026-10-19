
namespace Milestone02_OOP.Models;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public decimal DailyRate { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Car() { }

    public Car(int id, string make, string model, decimal dailyRate)
    {
        Id = id;
        Make = make;
        Model = model;
        DailyRate = dailyRate;
        IsAvailable = true;
    }
}