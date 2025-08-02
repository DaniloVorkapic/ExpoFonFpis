using System.ComponentModel;

namespace Backend.Enums
{
    public enum LifeCycleStatusEnum
    {
        [Description("Active")]
        Active = 1,
        [Description("Used")]
        Used = 2,
        [Description("Deactivated")]
        Deactivated = 3
    }

    public class LifeCycleEnum : EnumWrapper<LifeCycleStatusEnum>
    {
        public LifeCycleEnum() : this(default(LifeCycleStatusEnum)) { }

        public LifeCycleEnum(int value)
        {
            SetValue(value);
        }

        public LifeCycleEnum(LifeCycleStatusEnum type)
        {
            SetValue((int)type, type.ToString());
        }

        public static implicit operator int(LifeCycleEnum type)
        {
            return type.Value;
        }

        public static implicit operator LifeCycleEnum(int value)
        {
            return new LifeCycleEnum(value);
        }

        public static implicit operator LifeCycleStatusEnum(LifeCycleEnum type)
        {
            if (type is null)
            {
                return default;
            }

            return type.EnumValue;
        }

        public static implicit operator LifeCycleEnum(LifeCycleStatusEnum type)
        {
            return new LifeCycleEnum(type);
        }
    }
}
