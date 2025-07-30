using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.DTOs
{
    public record CreateRegistrationDto(
        long Id,
        string FirstName,
        string LastName,
        string Profession,
        AddressDto Address,
        string Email,
        string ConfirmEmail,
        bool IsGroupRegistration,
        int NumberOfPeople,
        bool IsArtReserved,
        bool IsPhotoReserved,
        decimal Price,
        string? PromoCode,
        bool HasPromoCode
     );
}
