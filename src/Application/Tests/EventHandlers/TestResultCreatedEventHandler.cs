using Application.Common.Interfaces;
using Application.Tests.Commands.ComputeTestResult;
using MediatR;

namespace Application.Tests.EventHandlers;

public class TestResultCreatedEventHandler : INotificationHandler<TestResultCreatedEvent>
{
    private readonly IEmailSender emailSender;

    public TestResultCreatedEventHandler(IEmailSender emailSender)
    {
        this.emailSender = emailSender;
    }

    public async Task Handle(TestResultCreatedEvent notification, CancellationToken cancellationToken)
    {
        await emailSender.Send("use@gmail.com", "subject", "test results");
    }
}