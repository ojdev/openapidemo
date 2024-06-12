namespace OpenAPIDemo.Infrastructure.Services;

public class ApiKeyService(ApplicationDbContext context) : IApiKeyService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ApiKey> GenerateApiKeyAsync(TimeSpan validPeriod, int usageLimit)
    {
        // 生成唯一的API Key
        var apiKey = new ApiKey
        {
            Key = Guid.NewGuid().ToString("N"),
            UsageCount = 0,
            ExpiryTime = DateTime.UtcNow.Add(validPeriod)
        };
        _context.ApiKeys.Add(apiKey);
        await _context.SaveChangesAsync();
        return apiKey;
    }

    public async Task<bool> ValidateApiKeyAsync(string key)
    {
        var apiKey = _context.ApiKeys.FirstOrDefault(a => a.Key == key);

        if (apiKey == null || apiKey.ExpiryTime < DateTime.UtcNow)
        {
            return false;
        }

        if (apiKey.UsageCount >= 100) //限制100次
        {
            return false;
        }

        // 如果API Key有效，增加使用次数
        apiKey.UsageCount++;
        await _context.SaveChangesAsync();
        return true;
    }
}