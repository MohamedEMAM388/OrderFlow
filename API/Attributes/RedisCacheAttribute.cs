using System.Text;
using Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Attributes;

public class RedisCacheAttribute : ActionFilterAttribute
{
    private readonly int _duration;
    public RedisCacheAttribute(int duration = 5)
    {
          _duration = duration;
    }

    public  override async Task OnActionExecutionAsync(ActionExecutingContext context
                                                , ActionExecutionDelegate next)
    {
        // get cache service
        var cacheService = context.HttpContext.RequestServices
                           .GetRequiredService<ICacheService>();
        
        // create key based on request path
        var cacheKey = CreateCacheKey(context.HttpContext.Request);
        
        // get data from cache 
        var cacheData = await cacheService.GetDataAsync(cacheKey);
        if (cacheData is not null)
        {
            context.Result = new ContentResult()
            {
                Content = cacheData,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK,

            };
            return;
        }
        
        var nextContext = await next.Invoke();
        if (nextContext.Result is OkObjectResult result)
        {
            await cacheService.SetDataAsync(cacheKey, result.Value! ,
                TimeSpan.FromMinutes(_duration));
        }

    }


    private static string CreateCacheKey(HttpRequest request)
    {
        var key = new StringBuilder();
        key.Append(request.Path);

        foreach (var item in request.Query.OrderBy(x => x.Key))
        {
            key.Append($"{item.Key}-{item.Value}");
            
        }
        
        
        return key.ToString();
    }
}