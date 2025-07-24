namespace Backend.Entities
{
    public class ReturnDate
    {
        public DateTime Date { get; private set; }

        private ReturnDate(DateTime date)
        {
            Date = date;
        }

        public static ReturnDate Create(DateTime date)
        {
            return new ReturnDate(date);
        }
    }
}
