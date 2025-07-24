namespace Backend.DTOs
{
    public record MaleEmployeeDto(
        long Id,
        string FirstName,
        string LastName,
        string Description,
        List<ChildDto> Children) : EmployeeDto(Id, FirstName, LastName, Description, Children);
}
