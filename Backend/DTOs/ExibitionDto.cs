namespace Backend.DTOs
{
    public record ExibitionDto(
        long Id,
        string Title,
        string StartTime,
        string EndTime,
        string Artist);
}
