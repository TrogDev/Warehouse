using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Resources.GetResourceList;

public record GetResourceListQuery : IRequest<IEnumerable<Resource>> { }
