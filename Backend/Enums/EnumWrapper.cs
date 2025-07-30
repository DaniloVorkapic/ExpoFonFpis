using Backend.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Backend.Enums
{
    [Serializable]
    [DataContract]
    public abstract class EnumWrapper<TEnum> : EnumWrapper
       where TEnum : struct
    {
        [NonSerialized]
        Type enumType = null;

        public EnumWrapper()
        {
            enumType = typeof(TEnum);

            if (!enumType.IsEnum)
            {
                throw new Exception();
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        [NotMapped]
        public TEnum EnumValue
        {
            get
            {
                return Value.ToEnum<TEnum>(default(TEnum));
            }
        }

        public virtual void SetValue(int value, string name = null)
        {
            this.Value = value;
        }
    }

    [DataContract]
    [Serializable]
    public class EnumWrapper
    {
        [DataMember]
        public int Value { get; set; }

        public void SetObjectValue(object value)
        {
            if (value == null)
                return;
            try
            {
                this.Value = (int)value;
            }
            catch
            {
            }
        }
    }
}
