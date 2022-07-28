using System.Threading.Tasks;
using Application.IntegrationTests.Common;
using Application.Tests.Commands.ComputeTestResult;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Tests.Commands.ComputeTestResult;

public class ComputeTestResultCommandHandlerTests : BaseTests
{
    [Test]
    public async Task WhenCalled_CreatesNewTestResult()
    {
        // arrange
        var command = new ComputeTestResultCommand
        {
            TestTemplateId = 1,
            Answers = new[]
            {
                new QuestionAnswer
                {
                    AnswerId = 2,
                    QuestionId = 1
                },
                new QuestionAnswer
                {
                    AnswerId = 6,
                    QuestionId = 2
                },
                new QuestionAnswer
                {
                    AnswerId = 10,
                    QuestionId = 3
                }
            }
        };

        // act
        var actualDto = await SendRequest(command);

        // assert
        actualDto.Should().NotBeNull();

        var actualEntity = GetEntityById<TestResult>(actualDto.Id);

        actualEntity.Should().NotBeNull();
    }
}