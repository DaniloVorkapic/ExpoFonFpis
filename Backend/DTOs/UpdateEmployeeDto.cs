namespace Backend.DTOs
{
    public record UpdateEmployeeDto(long EmployeeId, string? FirstName, string? LastName, string? Description);
}
