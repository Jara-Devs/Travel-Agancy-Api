using Travel_Agency_Domain.Others;
using Travel_Agency_Domain.Users;

namespace Travel_Agency_Domain.Payments;

public class ReserveTourist : Reserve
{
    public int UserId { get; set; }

    public Tourist User { get; set; } = null!;

    public ReserveTourist(int packageId, ICollection<UserIdentity> userIdentities, int paymentOrderId,
        int userId) : base(
        packageId, userIdentities, paymentOrderId)
    {
        this.UserId = userId;
    }
}