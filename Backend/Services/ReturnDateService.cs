using Backend.Entities;
using Backend.Enums;
using Backend.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
    public class ReturnDateService : IReturnDateService
    {
        private readonly IGenericRepository<FemaleEmployee> _employeeRepository;
        private readonly IGenericRepository<Holiday> _holidayRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnDateService(IGenericRepository<FemaleEmployee> employeeRepository, IGenericRepository<Holiday> holidayRepository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _holidayRepository = holidayRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task RecalculateReturnDateForEmployee(long employeeId)
        {
            var employee = await _employeeRepository.GetQueryable()
                .Include(e => e.Pregnancies)
                .ThenInclude(p => p.Leaves)
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                return;
            }

            var returnDate = ReturnDate.Create(CalculateReturnDate(employee));
            employee.SetReturnDate(returnDate);

            await _unitOfWork.CommitAsync();
        }

        public async Task RecalculateReturnDateForAllEmployees()
        {
            var employees = await _employeeRepository.GetQueryable()
                .Include(e => e.Pregnancies)
                .ThenInclude(p => p.Leaves)
                .ToListAsync();

            foreach (var employee in employees)
            {
                var returnDate = ReturnDate.Create(CalculateReturnDate(employee));
                employee.SetReturnDate(returnDate);
            }

            await _unitOfWork.CommitAsync();
        }

        private DateTime CalculateReturnDate(FemaleEmployee employee)
        {
            var pregnancy = employee.Pregnancies.LastOrDefault(p => p.IsActive);

            if (pregnancy == null)
            {
                return DateTime.MinValue;
            }

            var endDate = CalculateEndDate(pregnancy);

            while (IsNonWorkingDay(endDate))
            {
                endDate = endDate.AddDays(1);
            }

            return endDate;
        }

        private bool IsNonWorkingDay(DateTime endDate)
        {
            return endDate.DayOfWeek == DayOfWeek.Saturday || endDate.DayOfWeek == DayOfWeek.Sunday || IsDateHoliday(endDate);
        }

        private DateTime CalculateEndDate(Pregnancy pregnancy)
        {
            var startDate = pregnancy.DateOfChildbirth;

            return pregnancy.Leaves.Aggregate(startDate, GetEndDateForLeave);
        }

        private DateTime GetEndDateForLeave(DateTime endDate, Leave leave)
        {
            endDate = GetEndDateOfLeave(leave, endDate);

            if (leave.LeaveType == LeaveType.Godisnji)
            {
                var startDate = GetStartDateOfLeave(leave, endDate);
                endDate = ExtendForWeekendsAndHolidays(startDate, endDate);
            }

            return endDate;
        }

        private static DateTime GetStartDateOfLeave(Leave leave, DateTime startDate)
        {
            var strategy = LeaveDurationStrategyFactory.GetStrategy(leave.DurationType);
            return strategy.CalculateStartDateOfLeave(startDate, leave.Duration);
        }

        private static DateTime GetEndDateOfLeave(Leave leave, DateTime endDate)
        {
            var strategy = LeaveDurationStrategyFactory.GetStrategy(leave.DurationType);
            return strategy.CalculateEndDateOfLeave(endDate, leave.Duration);
        }

        private DateTime ExtendForWeekendsAndHolidays(DateTime startDate, DateTime endDate)
        {
            var dates = GetAllDatesBetween(startDate, endDate);

            return dates
                .Where(IsNonWorkingDay)
                .Aggregate(endDate, (current, _) => current.AddDays(1));
        }

        private bool IsDateHoliday(DateTime date)
        {
            var holidays = _holidayRepository.GetQueryable().ToList();

            return holidays.Any(holiday => holiday.Dates.Any(d => d.Date == date.Date));
        }

        private static IEnumerable<DateTime> GetAllDatesBetween(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                yield return date;
            }
        }

    }

    internal interface ILeaveDurationStrategy
    {
        public DateTime CalculateStartDateOfLeave(DateTime startDate, int duration);
        public DateTime CalculateEndDateOfLeave(DateTime endDate, int duration);
    }

    internal class DaysLeaveDurationStrategy : ILeaveDurationStrategy
    {
        public DateTime CalculateStartDateOfLeave(DateTime startDate, int duration)
        {
            return startDate.AddDays(-duration);
        }

        public DateTime CalculateEndDateOfLeave(DateTime endDate, int duration)
        {
            return endDate.AddDays(duration);
        }
    }
    internal class WeeksLeaveDurationStrategy : ILeaveDurationStrategy
    {
        public DateTime CalculateStartDateOfLeave(DateTime startDate, int duration)
        {
            return startDate.AddDays(-duration * 7);
        }

        public DateTime CalculateEndDateOfLeave(DateTime endDate, int duration)
        {
            return endDate.AddDays(duration * 7);
        }
    }
    internal class MonthsLeaveDurationStrategy : ILeaveDurationStrategy
    {
        public DateTime CalculateStartDateOfLeave(DateTime startDate, int duration)
        {
            return startDate.AddMonths(-duration);
        }

        public DateTime CalculateEndDateOfLeave(DateTime endDate, int duration)
        {
            return endDate.AddMonths(duration);
        }
    }

    internal class LeaveDurationStrategyFactory
    {
        public static ILeaveDurationStrategy GetStrategy(DurationType durationType)
        {
            return durationType switch
            {
                DurationType.Days => new DaysLeaveDurationStrategy(),
                DurationType.Weeks => new WeeksLeaveDurationStrategy(),
                DurationType.Months => new MonthsLeaveDurationStrategy(),
                _ => throw new ArgumentException("Invalid duration type")
            };
        }
    }


}
