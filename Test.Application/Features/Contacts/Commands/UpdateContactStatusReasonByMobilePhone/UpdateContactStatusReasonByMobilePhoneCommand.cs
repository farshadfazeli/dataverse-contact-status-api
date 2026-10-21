using MediatR;

namespace Test.Application.Features.Contacts.Commands.UpdateContactStatusReasonByMobilePhone;

public sealed record UpdateContactStatusReasonByMobilePhoneCommand(string MobilePhone) : IRequest<bool>;
