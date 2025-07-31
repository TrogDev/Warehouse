using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Queries.Clients.GetClientList;

public record GetClientListQuery : IRequest<IEnumerable<Client>> { }
