using Backend.Commands;
using Backend.DTOs;
using Backend.Queries;
using Backend.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/holidays")]
    [ApiController]
    public class HolidaysController : BaseController
    {
        public HolidaysController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHolidays()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllHolidaysQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateHoliday([FromBody] CreateHolidayDto holidayDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateHolidayCommand(holidayDto)));
        }

        [HttpDelete("{holidayId:long}")]
        public async Task<IActionResult> DeleteHoliday([FromRoute] long holidayId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeleteHolidayCommand(holidayId)));
        }
    }
}
