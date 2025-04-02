using FixManager.Api.Common.Api;
using FixManager.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.ConfigurationDevEnvironment();

app.MapEndpoints();
app.Run();
