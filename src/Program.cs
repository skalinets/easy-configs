var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("my_config.json", optional: false, reloadOnChange: true);
var app = builder.Build();

app.MapGet("/", () => $"Hello World! The name is {app.Configuration["name"]}.");

app.Run();
