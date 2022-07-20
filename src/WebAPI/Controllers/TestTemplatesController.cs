using Application.TestTemplates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("templates")]
public class TestTemplatesController:ControllerBase
{
    private readonly ISender sender;

    public TestTemplatesController(ISender sender)
    {
        this.sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await sender.Send(new GetTestTemplatesQuery());

        return Ok(result);
    }
}