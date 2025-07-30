namespace Backend.DTOs
{
    public record RegistrationByIdDto(
        string FirstName,
        string LastName,
        string Profession,
        string Address1,
        string Address2,
        string PostalCode,
        string City,
        string Country,
        string Email,
        string RegistrationType,
        int NumberOfPeople,
        bool ReserveArt,
        bool ReservePhoto,
        decimal TotalPrice,
        string? PromoCode,
        bool HsPromoCodeS
        );
}
