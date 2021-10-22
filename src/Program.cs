using Microsoft.AspNetCore;

static string GetName(HttpContext ctx) => ctx.RequestServices.GetService<IConfiguration>()["name"];

var app = WebHost.CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile("my_config.json", optional: false, reloadOnChange: true))
    .Configure((ctx, builder) => builder
           .UseRouting()
           .UseEndpoints(
               e => e.MapGet("/", c => c.Response.WriteAsync($"Hello World! The name is {GetName(c)}."))))
    .Build();

app.Run();
