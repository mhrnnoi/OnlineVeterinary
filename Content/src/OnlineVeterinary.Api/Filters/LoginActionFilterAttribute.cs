using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineVeterinary.Api.Filters;
public class LoginActionFilterAttribute : Attribute, IAsyncActionFilter
{
    private readonly ILogger<GlobalFilter> _logger;
    public LoginActionFilterAttribute(ILogger<GlobalFilter> logger)
    {
        _logger = logger;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _logger.LogInformation("an attempt was made to Login, by the way  im LoginAction filter :)");
        await next();
        _logger.LogInformation("im LoginAction filter :) and im sure the request is responded and finished");

    }
}