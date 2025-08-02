using Backend.Queries;
using Backend.Utils;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Backend.DTOs;
using Backend.Commands;

namespace Backend.Controllers
{
    [Route("api/manifestations")]
    [ApiController]
    public class ManifestationsController : BaseController
    {
        public ManifestationsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("exibitions/{exibitionType:int}")]
        public async Task<IActionResult> GetAllExibitions([FromRoute] int exibitionType)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllExibitionsQuery(exibitionType)));
        }

        [HttpGet]
        public async Task<IActionResult> GetFonManifestation()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetFonManifestationQuery()));
        }

        [HttpGet("registrations")]
        public async Task<IActionResult> GetRegistrations()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetAllRegistrationsQuery()));
        }

        [HttpGet("prices")]
        public async Task<IActionResult> GetPrices()
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetPricesQuery()));
        }

        [HttpGet("calculatedPrices")]
        public async Task<IActionResult> GetCalculatedPrice([FromQuery] GetCalculatedDto calculatedDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetCalculatedPriceQuery(calculatedDto)));
        }

        [HttpPost("registrations")]
        public async Task<IActionResult> AddOrUpdateRegistration([FromBody] CreateRegistrationDto registrationDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new CreateRegistrationCommand(registrationDto)));
        }

        [HttpGet("registrations/{id:long}")]
        public async Task<IActionResult> GetRegistrationById([FromRoute] long id)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetRegistrationByIdQuery(id)));
        }

        [HttpPost("deactivateRegistration")]
        public async Task<IActionResult> DeactivateRegistration([FromBody] DeactivateRegistrationDto registrationDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new DeactivateRegistrationCommand(registrationDto)));
        }

        [HttpGet("confirmation")]
        public async Task<IActionResult> GetConfirmation([FromQuery] GetConfirmationDto confirmationDto)
        {
            return ResultHandler.HandleResult(await Mediator.Send(new GetConfimrationQuery(confirmationDto)));
        }

    }
}
