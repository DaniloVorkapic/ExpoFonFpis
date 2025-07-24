namespace Backend.Entities
{
    public class Holiday : BaseEntity
    {
        public string Name { get; private set; }
        public List<DateTime> Dates { get; private set; }
        public string Description { get; private set; }

        private Holiday(string name, List<DateTime> dates, string description)
        {
            Name = name;
            Dates = dates;
            Description = description;
        }

        public static Holiday Create(string name, List<DateTime> dates, string description)
        {
            return new Holiday(name, dates, description);
        }

    }
}
