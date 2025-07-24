namespace Backend.Entities
{
    public class FemaleEmployee : Employee
    {
        public List<Pregnancy> Pregnancies { get; private set; } = [];
        public ReturnDate? ReturnDate { get; private set; }

        private FemaleEmployee(string firstName, string lastName, string description) : base(firstName, lastName, description)
        {
        }

        public static FemaleEmployee Create(string firstName, string lastName, string description)
        {
            return new FemaleEmployee(firstName, lastName, description);
        }

        public void SetReturnDate(ReturnDate returnDate)
        {
            ReturnDate = returnDate;
        }
    }
}
