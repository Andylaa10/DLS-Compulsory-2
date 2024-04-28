using Monitoring;
using OpenTelemetry.Trace;
using PatientService.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var serviceName = "PatientService";
var serviceVersion = "1.0.0";


builder.Services.AddOpenTelemetry().Setup(serviceName, serviceVersion);
builder.Services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));
builder.Services.ConfigureDependencyInjection();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(static x => x
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();