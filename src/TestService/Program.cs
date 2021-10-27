using Microsoft.AspNetCore;

static string GetName(HttpContext ctx) => ctx.RequestServices.GetService<IConfiguration>()["name"];

var startTime = DateTime.Now;

const string ConfigPath ="/config/my_config.json";

while (!File.Exists(ConfigPath)) { Thread.Sleep(1000); }

var app = WebHost.CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile(ConfigPath, optional: true, reloadOnChange: true))
    .Configure((ctx, builder) => builder
           .UseRouting()
           .UseEndpoints(
               e => e.MapGet("/", c => c.Response.WriteAsync($"Hello World! The name is {GetName(c)}. Uptime is {DateTime.Now.Subtract(startTime)}. Some stats: {new {Environment.ProcessorCount, Memory = GC.GetTotalMemory(true)}}"))))
    .Build();

app.Run();
