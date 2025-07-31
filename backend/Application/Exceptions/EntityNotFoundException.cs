namespace Warehouse.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
        : base("Entity not found") { }
}
