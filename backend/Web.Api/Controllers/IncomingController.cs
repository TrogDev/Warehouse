using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands.Incomings.CreateIncoming;
using Warehouse.Application.Commands.Incomings.DeleteIncoming;
using Warehouse.Application.Commands.Incomings.EditIncoming;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Queries.Incomings.GetIncoming;
using Warehouse.Application.Queries.Incomings.GetIncomingList;
using Warehouse.Web.Api.Requests;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/incomings")]
public class IncomingController : ControllerBase
{
    private readonly ISender mediator;

    public IncomingController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetIncomingListQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetIncomingQuery() { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateIncomingCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        try
        {
            await mediator.Send(new DeleteIncomingCommand() { Id = id });
        }
        catch (NotEnoughResourcesException)
        {
            return BadRequest(
                new { Resources = new string[] { "Недостаточно ресурсов на складе" } }
            );
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditIncomingBody body)
    {
        try
        {
            await mediator.Send(
                new EditIncomingCommand()
                {
                    Id = id,
                    Number = body.Number,
                    Date = body.Date,
                    Items = body.Items
                }
            );
        }
        catch (NotEnoughResourcesException)
        {
            return BadRequest(
                new { Resources = new string[] { "Недостаточно ресурсов на складе" } }
            );
        }

        return NoContent();
    }
}
