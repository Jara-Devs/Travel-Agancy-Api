using Travel_Agency_Core;
using Travel_Agency_Domain.Images;

namespace Travel_Agency_Domain.Services;

public class TouristActivity : Entity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Image Image { get; set; } = null!;  

    public Guid ImageId { get; set; }

    public TouristActivity(string name, string description, Guid imageId)
    {
        this.Name = name;
        this.Description = description;
        this.ImageId = imageId;
    }

    public ICollection<Excursion> Excursions { get; set; } = null!;
}