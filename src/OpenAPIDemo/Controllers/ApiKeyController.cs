namespace OpenAPIDemo.Controllers;
/// <summary>
/// apikey管理
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ApiKeyController : ControllerBase
{
    private readonly IApiKeyService _apiKeyService;

    public ApiKeyController(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
    }
    /// <summary>
    /// 申请一个新的apikey
    /// </summary>
    /// <returns></returns>
    [HttpPost("apply")]
    public async Task<ActionResult<ApiKey>> ApplyForKey()
    {
        // 生成新的API Key
        var apiKey = await _apiKeyService.GenerateApiKeyAsync(TimeSpan.FromDays(30), 1000); // 例如：有效期为30天，使用次数限制为1000次

        // 将API Key关联到用户
        // 这里可以将API Key存储到用户的数据库记录中，或者创建一个新的表来关联用户和API Key

        return apiKey;
    }
}