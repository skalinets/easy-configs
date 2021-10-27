using Microsoft.AspNetCore;
using TestService;

var startTime = DateTime.Now;

string GetContent(HttpContext context) =>
   string.Join('\n', context
                         .RequestServices
                         .GetService<IConfiguration>().GetSection("sessions")
                         .Get<List<Session>>()
                         .Select(s => $"{s.Name} [{s.Level}]"))

   + $"\n\nUptime is {DateTime.Now.Subtract(startTime)}";

const string ConfigPath ="/config/sessions.json";

while (!File.Exists(ConfigPath)) { Thread.Sleep(1000); }

var app = WebHost.CreateDefaultBuilder()
    .ConfigureAppConfiguration(x => x.AddJsonFile(ConfigPath, optional: true, reloadOnChange: true))
    .Configure((ctx, builder) => builder
           .UseRouting()
           .UseEndpoints(
               e => e.MapGet("/", c => c.Response.WriteAsync(GetContent(c)))))
    .Build();

app.Run();
