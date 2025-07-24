using Backend.Commands;
using Backend.DTOs;
using Backend.Queries;
using Backend.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        public EmployeesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] string? nameFilter)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllEmployeesQuery(nameFilter)));
        }

        [HttpGet("male")]
        public async Task<IActionResult> GetAllMaleEmployees()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllMaleEmployeesQuery()));
        }

        [HttpGet("female")]
        public async Task<IActionResult> GetAllFemaleEmployees()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllFemaleEmployeesQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateEmployeeCommand(employeeDto)));
        }

        [HttpGet("{employeeId:long}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] long employeeId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetEmployeeByIdQuery(employeeId)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new UpdateEmployeeCommand(updateEmployeeDto)));
        }

        [HttpGet("{employeeId:long}/children")]
        public async Task<IActionResult> GetAllChildren([FromRoute] long employeeId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllChildrenQuery(employeeId)));
        }

        [HttpPost("children")]
        public async Task<IActionResult> CreateChild([FromBody] CreateChildDto childDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateChildCommand(childDto)));
        }

        [HttpDelete("{employeeId:long}/children/{childId:long}")]
        public async Task<IActionResult> DeleteChildFromEmployee([FromRoute] long employeeId, [FromRoute] long childId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeleteChildCommand(employeeId, childId)));
        }

        [HttpGet("{employeeId:long}/pregnancies")]
        public async Task<IActionResult> GetAllPregnancies([FromRoute] long employeeId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllPregnanciesQuery(employeeId)));
        }

        [HttpGet("{employeeId:long}/pregnancies/{pregnancyId:long}")]
        public async Task<IActionResult> GetPregnancyById([FromRoute] long employeeId, [FromRoute] long pregnancyId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetPregnancyByIdQuery(employeeId, pregnancyId)));
        }

        [HttpPost("pregnancies")]
        public async Task<IActionResult> CreatePregnancy([FromBody] CreatePregnancyDto pregnancyDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreatePregnancyCommand(pregnancyDto)));
        }

        [HttpPut("pregnancies")]
        public async Task<IActionResult> UpdatePregnancy([FromBody] UpdatePregnancyDto pregnancyDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new UpdatePregnancyCommand(pregnancyDto)));
        }

        [HttpDelete("{employeeId:long}/pregnancies/{pregnancyId:long}")]
        public async Task<IActionResult> DeletePregnancy([FromRoute] long employeeId, [FromRoute] long pregnancyId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeletePregnancyCommand(employeeId, pregnancyId)));
        }

        [HttpPost("pregnancies/leaves")]
        public async Task<IActionResult> CreateLeave([FromBody] CreateLeaveDto leaveDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateLeaveCommand(leaveDto)));
        }
        
        [HttpDelete("{employeeId:long}/pregnancies/{pregnancyId:long}/leaves/{leaveId:long}")]
        public async Task<IActionResult> DeleteLeave([FromRoute] long employeeId, [FromRoute] long pregnancyId, [FromRoute] long leaveId)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeleteLeaveCommand(employeeId, pregnancyId, leaveId)));
        }

    }
}
