using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.IntegrationTests.Common;
using Application.TestTemplates.Queries.GetTestTemplateById;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.TestTemplates.Queries.GetTestTemplateById;

public class GetTestTemplateByIdTests : BaseTests
{
    [Test]
    public async Task WhenEntityExists_ReturnTheCorrectTestTemplate()
    {
        // arrange  
        var query = new GetTestTemplateByIdQuery(1);

        // act
        var actual = await SendRequest(query);

        // assert
        var expected = Map<TestTemplate, TestTemplateDto>(TestDataBaseSeeder.PersonalityTest);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task WhenDoesNotExist_ThrowsEntityNotFundException()
    {
        // arrange
        const int invalidId = 2;
        var query = new GetTestTemplateByIdQuery(invalidId);

        // act
        var act = () => SendRequest(query);

        // assert
        await act.Should().ThrowAsync<EntityNotFoundException>()
            .WithMessage($"Entity \"{nameof(TestTemplate)}\" with id ({invalidId}) was not found.");
    }
}