using System.ComponentModel;

namespace Backend.Enums
{
    public enum ExibitionTypeStatus
    {
        [Description("None")]
        None = 0,
        [Description("Art")]
        Art = 1,
        [Description("Photo")]
        Photo = 2
    }

    public class ExibitionTypeEnum : EnumWrapper<ExibitionTypeStatus>
    {
        public ExibitionTypeEnum() : this(default(ExibitionTypeStatus)) { }

        public ExibitionTypeEnum(int value)
        {
            SetValue(value);
        }

        public ExibitionTypeEnum(ExibitionTypeStatus type)
        {
            SetValue((int)type, type.ToString());
        }

        public static implicit operator int(ExibitionTypeEnum type)
        {
            return type.Value;
        }

        public static implicit operator ExibitionTypeEnum(int value)
        {
            return new ExibitionTypeEnum(value);
        }

        public static implicit operator ExibitionTypeStatus(ExibitionTypeEnum type)
        {
            if (type is null)
            {
                return default;
            }

            return type.EnumValue;
        }

        public static implicit operator ExibitionTypeEnum(ExibitionTypeStatus type)
        {
            return new ExibitionTypeEnum(type);
        }
    }
}
