using Backend.Enums;

namespace Backend.Entities
{
    public class Leave : BaseEntity
    {
        public LeaveType LeaveType { get; private set; }
        public DurationType DurationType { get; private set; }
        public int Duration { get; private set; }

        private Leave(LeaveType leaveType, DurationType durationType, int duration)
        {
            LeaveType = leaveType;
            DurationType = durationType;
            Duration = duration;
        }

        public static Leave Create(LeaveType leaveType, DurationType durationType, int duration)
        {
            return new Leave(leaveType, durationType, duration);
        }
    }
}
