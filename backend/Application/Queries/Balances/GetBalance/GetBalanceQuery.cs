using MediatR;

namespace Warehouse.Application.Queries.Balances.GetBalance;

public record GetBalanceQuery : IRequest<int>
{
    public required long ResourceId { get; init; }
    public required long UnitId { get; init; }
}
