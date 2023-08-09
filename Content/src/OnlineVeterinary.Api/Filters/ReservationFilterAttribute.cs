using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineVeterinary.Api.Filters;
public class ReservationFilterAttribute : Attribute, IActionFilter
{
    private readonly ILogger<GlobalFilter> _logger;
    public ReservationFilterAttribute(ILogger<GlobalFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("an attempt was made to do something about Reservations by the way  im ReservationFilter filter :)");
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("im ReservationFilter filter :) and im sure the request is responded and finished");
        
    }
}