namespace Backend.Entities
{
    public class MaleEmployee : Employee
    {
        private MaleEmployee(string firstName, string lastName, string description) : base(firstName, lastName, description)
        {
        }

        public static MaleEmployee Create(string firstName, string lastName, string description)
        {
            return new MaleEmployee(firstName, lastName, description);
        }
    }
}
