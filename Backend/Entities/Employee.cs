namespace Backend.Entities
{
    public abstract class Employee : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Description { get; private set; }
        public List<Child> Children { get; private set; } = [];

        protected Employee(string firstName, string lastName, string description)
        {
            FirstName = firstName;
            LastName = lastName;
            Description = description;
        }

        public void Update(string? firstName = null, string? lastName = null, string? description = null)
        {
            FirstName = firstName ?? FirstName;
            LastName = lastName ?? LastName;
            Description = description ?? Description;
        }
    }
}
