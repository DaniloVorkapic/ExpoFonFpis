using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record DeleteHolidayCommand(long HolidayId) : BaseCommand<Result<Unit>>;
}
