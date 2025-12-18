using Employee_CQRS.API.Middlewares;
using Employee_CQRS.Application;
using Employee_CQRS.Application.Common.Behaviours;
using Employee_CQRS.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Serilog;

using Serilog.Debugging;

SelfLog.Enable(msg =>
{
    File.AppendAllText("serilog-selflog.txt", msg);
});

var builder = WebApplication.CreateBuilder(args);

// 🔹 Enable CORS for ALL origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ✅ Configure Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});

builder.Services.AddControllers();

// ✅ MediatR registration (THIS REGISTERS IMediator)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly);
});

// ✅ Pipeline behavior MUST be registered via DI
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehaviour<,>)
);

// ✅ FluentValidation
builder.Services.AddValidatorsFromAssembly(
    typeof(AssemblyReference).Assembly);

// Infrastructure (SQLite)
builder.Services.AddInfrastructure("Data Source=employee_cqrs.db");

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
