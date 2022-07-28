using Domain.Common;
using Domain.Entities;

namespace Application.Tests.Commands.ComputeTestResult;

public class TestResultCreatedEvent : DomainEventBase
{
    public TestResult TestResult { get; }

    public TestResultCreatedEvent(TestResult testResult)
    {
        TestResult = testResult;
    }
}