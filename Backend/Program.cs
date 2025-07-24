using System.Text.Json.Serialization;
using Backend.Extensions;
using Backend.Middleware;
using Backend.Middleware.Evidencija.Middleware;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureCqrs(builder.Configuration);
builder.Services.ConfigureValidators(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHangfireDashboard();

app.ConfigureNotificationJob();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("cors");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.Run();
