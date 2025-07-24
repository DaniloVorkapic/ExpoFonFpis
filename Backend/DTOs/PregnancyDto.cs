namespace Backend.DTOs
{
    public record PregnancyDto(long Id, DateTime DateOfOpeningPregnancy, DateTime DateOfChildBirth, bool IsActive, List<LeaveDto> Leaves);
}
