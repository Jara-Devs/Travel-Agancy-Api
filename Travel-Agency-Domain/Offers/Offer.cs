using Travel_Agency_Core;
using Travel_Agency_Domain.Packages;

namespace Travel_Agency_Domain.Offers;

public class Offer : Entity
{
    public string Name { get; set; } = null!;

    public int Availability { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public long StartDate { get; set; }

    public long EndDate { get; set; }

    public Offer(string description, double price, string name, int availability, long startDate, long endDate) {
        this.Description = description;
        this.Price = price;
        this.Name = name;
        this.Availability = availability;
        this.StartDate = startDate;
        this.EndDate = endDate;
    }
    
    public ICollection<Package> Packages { get; set; } = null!;
}