using Microsoft.AspNetCore;

static string GetName(HttpContext ctx) => ctx.RequestServices.GetService<IConfiguration>()["name"];

var startTime = DateTime.Now;

var app = WebHost.CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile("/config/my_config.json", optional: true, reloadOnChange: true))
    .Configure((ctx, builder) => builder
           .UseRouting()
           .UseEndpoints(
               e => e.MapGet("/", c => c.Response.WriteAsync($"Hello World! The name is {GetName(c)}. Uptime is {DateTime.Now.Subtract(startTime)}"))))
    .Build();

app.Run();
