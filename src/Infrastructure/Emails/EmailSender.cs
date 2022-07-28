using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Emails;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        this.logger = logger;
    }

    public Task Send(string to, string subject, string message)
    {
        logger.LogInformation("Email was sent successfully.");

        return Task.CompletedTask;
    }
}