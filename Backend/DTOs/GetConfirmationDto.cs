namespace Backend.DTOs
{
    public record GetConfirmationDto(
        long Id,
        string Email,
        string Identificator
    );
}
