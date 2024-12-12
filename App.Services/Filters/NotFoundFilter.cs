using App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Services.Filters;

public class NotFoundFilter<T, TId> : Attribute, IAsyncActionFilter where T : class where TId : struct
{
    private readonly IGenericRepository<T, TId> _genericRepository;
    public NotFoundFilter(IGenericRepository<T, TId> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var IdValue = context.ActionArguments.TryGetValue("id", out var idAsObject) ? idAsObject : null;
        
        if(idAsObject is not TId id)
        {
            await next();
            return;
        }

        if(await _genericRepository.AnyAsync(id))
        {
            await next();
            return;
        }

        var entityName = typeof(T).Name;
        var actionName = context.ActionDescriptor.RouteValues["action"];

        var result = ServiceResult.Fail($"data is not found.({entityName})({actionName})");
        context.Result = new NotFoundObjectResult(result);
    }
}