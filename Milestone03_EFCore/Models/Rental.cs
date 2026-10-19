namespace Milestone03_EFCore.Models;

public class Rental
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public int RentalDays { get; set; }
    public decimal TotalPrice { get; set; }

    public Rental() { }

    public Rental(int id, int carId, int customerId, int rentalDays, decimal dailyRate)
    {
        Id = id;
        CarId = carId;
        CustomerId = customerId;
        StartDate = DateTime.Now;
        RentalDays = rentalDays;
        TotalPrice = CalculateTotalPrice(dailyRate, rentalDays);
    }

    public static decimal CalculateTotalPrice(decimal dailyRate, int days)
    {
        return dailyRate * days;
    }
}