using Test.Application.Services.Contacts;
using MediatR;

namespace Test.Application.Features.Contacts.Commands.UpdateContactStatusReasonByMobilePhone;

public sealed class UpdateContactStatusReasonByMobilePhoneCommandHandler(
    IContactStatusService contactStatusService)
    : IRequestHandler<UpdateContactStatusReasonByMobilePhoneCommand, bool>
{
    public Task<bool> Handle(
        UpdateContactStatusReasonByMobilePhoneCommand request,
        CancellationToken cancellationToken) =>
        contactStatusService.UpdateStatusReasonByMobilePhoneAsync(
            request.MobilePhone,
            cancellationToken);
}
