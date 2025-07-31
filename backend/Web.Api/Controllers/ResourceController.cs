using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands.Resources.CreateResource;
using Warehouse.Application.Commands.Resources.DeleteResource;
using Warehouse.Application.Commands.Resources.EditResource;
using Warehouse.Application.Queries.Resources.GetResource;
using Warehouse.Application.Queries.Resources.GetResourceList;
using Warehouse.Web.Api.Requests;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/resources")]
public class ResourceController : ControllerBase
{
    private readonly ISender mediator;

    public ResourceController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetResourceListQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetResourceQuery() { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResourceCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditResourceBody body)
    {
        EditResourceCommand command =
            new()
            {
                Id = id,
                Name = body.Name,
                Status = body.Status
            };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        DeleteResourceCommand command = new() { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
