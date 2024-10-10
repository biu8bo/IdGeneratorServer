 
using IdGeneratorServer.Web;
using Serilog;
using Serilog.Events;

// 配置SerLog
var loggerConfig = new LoggerConfiguration()
#if DEBUG
    .MinimumLevel.Information()
#else
                .MinimumLevel.Information()
#endif
    //.MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
    //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
    .MinimumLevel.Override("AbpVnext.WebApi", LogEventLevel.Information)
    .Enrich.FromLogContext() // 上下文日志依次输出
    .WriteTo.Async(c => c.Console());

//使用 serLog
Log.Logger = loggerConfig.CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(builder.Configuration["App:SelfUrl"]);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac().UseSerilog();
await builder.AddApplicationAsync<WebModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
