namespace Application.Common.Interfaces;

public interface IEmailSender
{
    Task Send(string to, string subject, string message);
}