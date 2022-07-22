using Application.TestTemplates.Queries.GetTestTemplateById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("templates")]
public class TestTemplatesController : ControllerBase
{
    private readonly ISender sender;

    public TestTemplatesController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await sender.Send(new GetTestTemplateByIdQuery(id));

        return Ok(result);
    }
}