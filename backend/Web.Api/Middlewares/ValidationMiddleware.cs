using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Exceptions;

namespace Warehouse.Web.Api.Middlewares;

public class ValidationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            var response = new BadRequestObjectResult(e.Errors);
            await response.ExecuteResultAsync(new ActionContext { HttpContext = context });
        }
    }
}
