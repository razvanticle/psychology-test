using Application.Tests.Commands.ComputeTestResult;
using Application.Tests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("tests")]
public class TestsController : ControllerBase
{
    private readonly ISender sender;

    public TestsController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await sender.Send(new GetTestByIdQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComputeTestResultCommand command)
    {
        var result = await sender.Send(command);

        return Ok(result);
    }
}