using AspNet.Module.Common;

namespace AspNet.Module.Logging.Sample;

internal class SampleModule : IAspNetModule
{
    public void ConfigureApp(WebApplication app)
    {
    }

    public void Configure(AspNetModuleContext ctx) => ctx.Services.AddHostedService<TestHostedService>();
}

internal class TestHostedService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken) =>
        // throw new NotImplementedException();
        Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}