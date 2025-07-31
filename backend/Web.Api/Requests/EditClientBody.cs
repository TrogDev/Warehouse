using Warehouse.Domain.Enums;

namespace Warehouse.Web.Api.Requests;

public record EditClientBody(string Name, string Address, Status Status = Status.Work);
