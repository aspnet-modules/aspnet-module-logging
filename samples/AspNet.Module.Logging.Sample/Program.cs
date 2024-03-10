using AspNet.Module.Logging;
using AspNet.Module.Host;
using AspNet.Module.Logging.Extensions;
using AspNet.Module.Logging.Sample;

var builder = AspNetWebApplication.CreateBuilder(args);
builder.RegisterModule<LoggingModule>();
builder.RegisterModule<SampleModule>();
var app = builder.Build();

await app.RunWithLogging();