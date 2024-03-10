using AspNet.Module.Logging.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Formatting.OpenSearch;

namespace AspNet.Module.Logging.Extensions;

/// <summary>
///     Расширения для <see cref="LoggerConfiguration" />
/// </summary>
public static class SerilogExtensions
{
    /// <summary>
    ///     Конфигурация Serilog
    /// </summary>
    public static LoggerConfiguration Configure(this LoggerConfiguration loggerConfiguration,
        IConfiguration configuration, IHostEnvironment env, SerilogOptions options)
    {
        if (options.EnableSelfLog)
        {
            SelfLog.Enable(Console.Error);
        }

        loggerConfiguration = loggerConfiguration
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.WithAssemblyName()
            .Enrich.WithAssemblyVersion()
            .Enrich.WithMachineName()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration);

        if (env.IsDevelopment())
        {
            loggerConfiguration = loggerConfiguration
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "Logs/log.txt", fileSizeLimitBytes: null);
        }
        else
        {
            loggerConfiguration = options.UseOpenSearchFormat
                ? loggerConfiguration
                    .WriteTo.Console(
                        new ExceptionAsObjectJsonFormatter(renderMessage: true),
                        standardErrorFromLevel: LogEventLevel.Error
                    )
                : loggerConfiguration
                    .WriteTo.Console(
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                        standardErrorFromLevel: LogEventLevel.Error
                    );
        }

        return loggerConfiguration;
    }
}