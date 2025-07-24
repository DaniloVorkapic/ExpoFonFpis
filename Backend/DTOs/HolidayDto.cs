namespace Backend.DTOs
{
    public record HolidayDto(long Id, string Name, List<DateTime> Dates, string Description);
}
