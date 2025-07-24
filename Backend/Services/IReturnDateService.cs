namespace Backend.Services
{
    public interface IReturnDateService
    {
        public Task RecalculateReturnDateForEmployee(long employeeId);
        public Task RecalculateReturnDateForAllEmployees();
    }
}
