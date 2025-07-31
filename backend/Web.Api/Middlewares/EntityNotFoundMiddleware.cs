using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Exceptions;

namespace Warehouse.Web.Api.Middlewares;

public class EntityNotFoundMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (EntityNotFoundException)
        {
            var response = new ObjectResult("Entity not found") { StatusCode = 404 };
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
