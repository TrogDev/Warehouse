using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands.Outgoings.CreateOutgoing;
using Warehouse.Application.Commands.Outgoings.DeleteOutgoing;
using Warehouse.Application.Commands.Outgoings.EditOutgoing;
using Warehouse.Application.Exceptions;
using Warehouse.Application.Queries.Outgoings.GetOutgoing;
using Warehouse.Application.Queries.Outgoings.GetOutgoingList;
using Warehouse.Web.Api.Requests;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/outgoings")]
public class OutgoingController : ControllerBase
{
    private readonly ISender mediator;

    public OutgoingController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetOutgoingListQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetOutgoingQuery() { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOutgoingCommand command)
    {
        try
        {
            await mediator.Send(command);
        }
        catch (NotEnoughResourcesException)
        {
            return BadRequest(
                new { Resources = new string[] { "Недостаточно ресурсов на складе" } }
            );
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        await mediator.Send(new DeleteOutgoingCommand() { Id = id });
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditOutgoingBody body)
    {
        try
        {
            await mediator.Send(
                new EditOutgoingCommand()
                {
                    Id = id,
                    IsSigned = body.IsSigned,
                    ClientId = body.ClientId,
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
