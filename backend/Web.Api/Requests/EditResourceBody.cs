using Warehouse.Domain.Enums;

namespace Warehouse.Web.Api.Requests;

public record EditResourceBody(string Name, Status Status = Status.Work);
