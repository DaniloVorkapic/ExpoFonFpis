using Backend.DTOs;
using Backend.Http;
using MediatR;

namespace Backend.Commands
{
    public record CreateHolidayCommand(CreateHolidayDto HolidayDto) : BaseCommand<Result<HolidayDto>>;
}
