using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OpenAPI Demo"
    });
    o.IncludeXmlComments(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"OpenAPIDemo.xml"));
});
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("OpenApidDmoDB"));
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<ValidateApiKeyAttribute>();

//TODO 如果需要所有接口都进行apikey鉴权，就使用下面的片段，否则，就使用[ServiceFilter(typeof(ValidateApiKeyAttribute))]多controller标识
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add<ValidateApiKeyAttribute>();
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
