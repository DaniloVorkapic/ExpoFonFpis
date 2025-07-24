namespace Backend.DTOs
{
    public record CreateChildDto(long EmployeeId, string FirstName, string LastName, DateTime DateOfBirth);
}
