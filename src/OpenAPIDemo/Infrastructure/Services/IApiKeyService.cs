namespace OpenAPIDemo.Infrastructure.Services;
public interface IApiKeyService
{
    Task<ApiKey> GenerateApiKeyAsync(TimeSpan validPeriod, int usageLimit);
    Task<bool> ValidateApiKeyAsync(string key);
}
