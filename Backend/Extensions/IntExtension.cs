namespace Backend.Extensions
{
    public static class IntExtensions
    {
        public static T ToEnum<T>(this int value, T defaultValue)
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException("T must be an Enum");
            }

            try
            {
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
