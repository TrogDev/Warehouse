using Warehouse.Domain.Enums;

namespace Warehouse.Web.Api.Requests;

public record EditUnitBody(string Name, Status Status = Status.Work);
