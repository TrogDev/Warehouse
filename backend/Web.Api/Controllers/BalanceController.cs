using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Queries.Balances.GetBalanceList;

namespace Warehouse.Web.Api.Controllers;

[ApiController]
[Route("/balances")]
public class BalanceController : ControllerBase
{
    private readonly ISender mediator;

    public BalanceController(ISender mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetBalanceListQuery query)
    {
        return Ok(await mediator.Send(query));
    }
}
