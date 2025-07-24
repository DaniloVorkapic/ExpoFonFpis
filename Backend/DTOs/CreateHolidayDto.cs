namespace Backend.DTOs
{
    public record CreateHolidayDto(string Name, List<DateTime> Dates, string Description);
}
