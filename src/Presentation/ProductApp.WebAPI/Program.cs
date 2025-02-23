using AspNetCoreRateLimit;
using ProductApp.Application;
using ProductApp.Infrastructure;
using ProductApp.Infrastructure.Middlewares;
using ProductApp.Persistence;
using ProductApp.Persistence.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Host.UseSerilog();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await ApplicationDbContext.InitializeDatabaseAsync(context);
}

#region RateLimit
var ipPolicyStore = app.Services.GetRequiredService<IIpPolicyStore>();
await ipPolicyStore.SeedAsync();
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSerilogRequestLogging();

//app.UseClientRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

