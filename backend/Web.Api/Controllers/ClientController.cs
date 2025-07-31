using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands.Clients.CreateClient;
using Warehouse.Application.Commands.Clients.DeleteClient;
using Warehouse.Application.Commands.Clients.EditClient;
using Warehouse.Application.Queries.Clients.GetClient;
using Warehouse.Application.Queries.Clients.GetClientList;
using Warehouse.Web.Api.Requests;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/clients")]
public class ClientController : ControllerBase
{
    private readonly ISender mediator;

    public ClientController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetClientListQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetClientQuery() { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditClientBody body)
    {
        EditClientCommand command =
            new()
            {
                Id = id,
                Name = body.Name,
                Address = body.Address,
                Status = body.Status
            };
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        DeleteClientCommand command = new() { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
