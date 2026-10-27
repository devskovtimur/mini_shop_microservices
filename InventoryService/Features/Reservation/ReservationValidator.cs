using Shared.Validating;

namespace InventoryService.Features.ReservedProduct;

public class ReservationValidator : BaseValidator<Reservation.Reservation>
{
    public ReservationValidator()
    {
        
    }
    
    public static readonly ReservationValidator Validator = new();
}