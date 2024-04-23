using MeasurementService.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var cors = "cors";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDependencyInjection();

builder.Services.AddCors(options =>
{
    options.AddPolicy(cors,policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();