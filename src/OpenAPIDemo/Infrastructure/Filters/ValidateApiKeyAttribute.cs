namespace OpenAPIDemo.Infrastructure.Filters;
public class ValidateApiKeyAttribute : ActionFilterAttribute
{
    private readonly IApiKeyService _apiKeyService;

    public ValidateApiKeyAttribute(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var apiKeyValues))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var key = apiKeyValues.FirstOrDefault();

        if (string.IsNullOrEmpty(key) || !await _apiKeyService.ValidateApiKeyAsync(key))
        {
            context.Result = new UnauthorizedResult();
        }
        await base.OnActionExecutionAsync(context, next);
    }
}