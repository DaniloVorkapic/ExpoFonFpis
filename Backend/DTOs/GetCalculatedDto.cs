namespace Backend.DTOs
{
    public record GetCalculatedDto(
        bool IsGroupRegistration,
        int? NumberOfPeople,
        bool ReserveArt,
        bool ReservePhoto,
        string? PromoCode,
        bool HasPromoCode
        );
}
