namespace Backend.DTOs
{
    public record RegistrationDto(
        long Id,
        string FirstName,
        string LastName,
        string Email,
        decimal? Price);
}
