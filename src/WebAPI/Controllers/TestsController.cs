using Application.Tests.Commands.ComputeTestResult;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ComputeTestResultCommand command)
    {
        var result = await sender.Send(command);

        return Ok(result);
    }
}