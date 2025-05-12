using Test.Config;
using Test.Infrastructure;
using Test.Services;
using Tutorial9.Repository;
using Tutorial9.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
    await initializer.InitializeAsync();
}

app.UseAuthorization();
app.MapControllers();
await app.RunAsync();