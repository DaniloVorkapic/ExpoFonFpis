namespace Backend.DTOs
{
    public record EmployeeDto(long Id, string FirstName, string LastName, string Description, List<ChildDto> Children)
    {
    }
}
