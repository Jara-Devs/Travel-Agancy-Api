using Travel_Agency_Core;
using Travel_Agency_Domain.Images;
using Travel_Agency_Domain.Others;

namespace Travel_Agency_Domain.Services;

public class TouristPlace : Entity
{
    public TouristPlace()
    {
    }

    public TouristPlace(string name, string description, Address address, Guid imageId)
    {
        Name = name;
        Description = description;
        Address = address;
        ImageId = imageId;
    }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public Image Image { get; set; } = null!;

    public Guid ImageId { get; set; }

    public ICollection<Excursion> Excursions { get; set; } = null!;

    public ICollection<Hotel> Hotels { get; set; } = null!;

    public ICollection<Flight> OriginFlights { get; set; } = null!;

    public ICollection<Flight> DestinationFlights { get; set; } = null!;
}