using AspDotNetLabs;
using AspDotNetLabs.Loggers;
using AspDotNetLabs.Middlewares;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appConfiguration = new ConfigurationBuilder().AddJsonFile("conf.json").Build();
builder.Services.Configure<Conf>(appConfiguration);
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var conf = app.Services.GetRequiredService<IOptions<Conf>>().Value;
loggerFactory.AddProvider(new FileLoggerProvider(conf.LogFile));

app.UseMiddleware<LoggerMiddleware>();

app.UseRouting();
app.UseSession();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($"Title: {conf.Title}, LogFile: {conf.LogFile}");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"default", 
        pattern: "{controller=Session}/{action=View}/{id?}");
    
    endpoints.MapControllerRoute(
        name:"withLang", 
        pattern: "{lang}/{controller=Session}/{action=View}/{id?}");
});
app.Run();
