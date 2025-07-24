using Backend.Enums;

namespace Backend.DTOs
{
    public record LeaveDto(long Id, LeaveType LeaveType, DurationType DurationType, int Duration)
    {
    }
}
