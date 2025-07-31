using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Balances.GetBalanceList;

public record GetBalanceListQuery : IRequest<IEnumerable<Balance>>
{
    public List<long>? Resources { get; init; }
    public List<long>? Units { get; init; }
}
