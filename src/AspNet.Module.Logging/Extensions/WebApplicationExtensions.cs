using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace AspNet.Module.Logging.Extensions;

/// <summary>
///     Расширение для <see cref="WebApplication" />
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    ///     Запуск с логированием
    /// </summary>
    public static async Task<int> RunWithLogging(this WebApplication app, string? url = null)
    {
        try
        {
            // https://github.com/serilog/serilog-aspnetcore#two-stage-initialization
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            var env = app.Environment;
            Log.Logger.Information($"🚀 Запуск сервиса {env.ApplicationName} [{env.EnvironmentName}] ...");
            await app.RunAsync(url);
            return 0;
        }
        catch (Exception e)
        {
            Log.Logger.Fatal(e, "🛑 Необработанная ошибка в процессе запуска сервиса.");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}