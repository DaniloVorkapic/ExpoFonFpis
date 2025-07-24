namespace Backend.Entities
{
    public class Pregnancy : BaseEntity
    {
        public DateTime DateOfOpeningPregnancy { get; private set; }
        public DateTime DateOfChildbirth { get; private set; }
        public bool IsActive { get; private set; }
        public List<Leave> Leaves { get; private set; } = [];

        private Pregnancy(DateTime dateOfOpeningPregnancy, DateTime dateOfChildbirth, bool isActive)
        {
            DateOfOpeningPregnancy = dateOfOpeningPregnancy;
            DateOfChildbirth = dateOfChildbirth;
            IsActive = isActive;
        }

        public static Pregnancy Create(DateTime dateOfOpeningPregnancy, DateTime dateOfChildbirth)
        {
            return new Pregnancy(dateOfOpeningPregnancy, dateOfChildbirth, true);
        }

        public void Update(DateTime? dateOfOpeningPregnancy = null)
        {
            DateOfOpeningPregnancy = dateOfOpeningPregnancy ?? DateOfOpeningPregnancy;
        }

    }
}
