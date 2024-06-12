namespace OpenAPIDemo.Infrastructure.Domains;
public class ApiKey
{
    public int Id { get; set; }
    public string Key { get; set; }
    public int UsageCount { get; set; }
    public DateTime ExpiryTime { get; set; }
}