namespace Warehouse.Application.Exceptions;

public class NotEnoughResourcesException : Exception
{
    public NotEnoughResourcesException()
        : base("Not enough resources") { }
}
