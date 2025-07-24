using Backend.Entities;

namespace Backend.DTOs
{
    public record FemaleEmployeeDto(
        long Id,
        string FirstName,
        string LastName,
        string Description,
        List<ChildDto> Children,
        List<PregnancyDto> Pregnancies,
        ReturnDate ReturnDate) : EmployeeDto(Id, FirstName, LastName, Description, Children);
}
