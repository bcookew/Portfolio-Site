using PortfolioSite.Host.Services;
using PortfolioSite.Host.UI;
using Serilog;
using Serilog.Sinks.MSSqlServer;

LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
.MinimumLevel.Warning()
.MinimumLevel.Override("PortfolioSite.Host", Serilog.Events.LogEventLevel.Information)
.WriteTo.Console();

string? loggingConnectionString = Environment.GetEnvironmentVariable("PortfolioSiteHost_ConnectionString");

if (!string.IsNullOrEmpty(loggingConnectionString))
{
    loggerConfiguration.WriteTo.MSSqlServer(connectionString: loggingConnectionString, sinkOptions: new MSSqlServerSinkOptions
    {
        TableName = "Logs",
        AutoCreateSqlTable = true
    });
}

Log.Logger = loggerConfiguration.CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents()
        .AddInteractiveWebAssemblyComponents();

    builder.Services.AddHttpClient();
    builder.Services.AddScoped<Web3FormsService>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.Use(async (context, next) =>
        {
            if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.Response.Headers.Append("Content-Security-Policy",
                    "base-uri 'self'; " +
                    "default-src 'self'; " +
                    "img-src data: https:; " +
                    "object-src 'none'; " +
                    "script-src 'self' 'unsafe-eval'; " +
                    "style-src 'self'; " +
                    "upgrade-insecure-requests;" +
                    "connect-src 'self'");
            }

            await next();
        });
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(PortfolioSite.ToDoApp._Imports).Assembly);

    app.Logger.LogInformation("Starting PortfolioSite.Host");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.ForContext<Program>().Warning("Shutting down PortfolioSite.Host");
    Log.CloseAndFlush();
}
