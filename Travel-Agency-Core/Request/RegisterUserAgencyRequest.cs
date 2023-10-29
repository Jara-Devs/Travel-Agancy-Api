namespace Travel_Agency_Core.Request;

public class RegisterUserAgencyRequest {
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int AgencyId { get; set; } = 0;
}