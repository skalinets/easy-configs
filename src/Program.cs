using Microsoft.Extensions.Options;
using Src;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("my_config.json", optional: false, reloadOnChange: true);
builder.Services.Configure<MySettings>(builder.Configuration.GetSection("Test"));

var app = builder.Build();

string GetHello()
{
    using var scope = app.Services.CreateScope();
    var foo =
        scope.ServiceProvider.GetService<IOptionsSnapshot<MySettings>>().Value.Name;
    return $"Hello World! The name is {foo}.";
}

app.MapGet("/", GetHello);

app.Run();
