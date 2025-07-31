using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Commands.Units.CreateUnit;
using Warehouse.Application.Commands.Units.DeleteUnit;
using Warehouse.Application.Commands.Units.EditUnit;
using Warehouse.Application.Queries.Units.GetUnit;
using Warehouse.Application.Queries.Units.GetUnitList;
using Warehouse.Web.Api.Requests;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/units")]
public class UnitController : ControllerBase
{
    private readonly ISender mediator;

    public UnitController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetUnitListQuery query)
    {
        return Ok(await mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        return Ok(await mediator.Send(new GetUnitQuery() { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUnitCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit([FromRoute] long id, [FromBody] EditUnitBody body)
    {
        EditUnitCommand command =
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
        DeleteUnitCommand command = new() { Id = id };
        await mediator.Send(command);
        return NoContent();
    }
}
