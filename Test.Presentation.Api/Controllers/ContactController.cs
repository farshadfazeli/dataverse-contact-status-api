using Test.Application.Features.Contacts.Commands.UpdateContactStatusReasonByMobilePhone;
using Test.Presentation.Api.Models.Contacts.Requests;
using Test.Presentation.Api.Models.Contacts.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Test.Presentation.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public sealed class ContactController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UpdateContactStatusReasonByMobilePhoneResponse>>
        UpdateStatusReasonByMobilePhone(
        [FromBody] UpdateContactStatusReasonByMobilePhoneRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateContactStatusReasonByMobilePhoneCommand(request.MobilePhone);
        var updated = await mediator.Send(command, cancellationToken);

        if (!updated)
            return NotFound();

        var response = new UpdateContactStatusReasonByMobilePhoneResponse(updated);
        return Ok(response);
    }
}
