using AspNet.Module.Common;
using AspNet.Module.Logging.Extensions;
using AspNet.Module.Logging.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Hosting;
using Serilog.Extensions.Logging;

namespace AspNet.Module.Logging;

/// <summary>
///     Модуль логирования
/// </summary>
public class LoggingModule : IAspNetModule
{
    private readonly LoggingConfig _config;

    public LoggingModule() : this(new LoggingConfig())
    {
    }

    public LoggingModule(LoggingConfig config)
    {
        _config = config;
    }

    public void ConfigureApp(WebApplication app)
    {
        if (_config.EnableRequestLogging)
        {
            app.UseSerilogRequestLogging();
        }
    }

    public void Configure(AspNetModuleContext ctx)
    {
        var loggerConfiguration = ConfigureSerilog(ctx.Configuration, ctx.Environment, new LoggerConfiguration());
        var logger = loggerConfiguration.CreateLogger();
        Log.Logger = logger;

        ctx.Services.AddSingleton<ILoggerFactory>(_ => new SerilogLoggerFactory(logger));
        ctx.Services.AddSingleton(logger);
        var diagnosticContext = new DiagnosticContext(logger);
        ctx.Services.AddSingleton(diagnosticContext);
        ctx.Services.AddSingleton<IDiagnosticContext>(sp => sp.GetRequiredService<DiagnosticContext>());
    }

    /// <summary>
    ///     Конфигурация логов
    /// </summary>
    protected virtual LoggerConfiguration ConfigureSerilog(IConfiguration configuration, IHostEnvironment env,
        LoggerConfiguration loggerConfiguration)
    {
        var serilogOptions = SerilogOptions.Default;
        configuration.GetSection(SerilogOptions.Key).Bind(serilogOptions, o => { o.BindNonPublicProperties = true; });
        return loggerConfiguration.Configure(configuration, env, serilogOptions);
    }
}