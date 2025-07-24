using Backend.Enums;

namespace Backend.DTOs
{
    public record CreateLeaveDto(long EmployeeId, long PregnancyId, LeaveType LeaveType, DurationType DurationType, int Duration);
}
