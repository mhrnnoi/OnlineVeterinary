using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineVeterinary.Api.Filters;
public class GlobalFilter : IActionFilter
{
    private readonly ILogger<GlobalFilter> _logger;
    public GlobalFilter(ILogger<GlobalFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("an attempt was made to do something by the way  im a global filter :)");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("im a global filter :) and im sure the request is responded and finished");
        
    }
}