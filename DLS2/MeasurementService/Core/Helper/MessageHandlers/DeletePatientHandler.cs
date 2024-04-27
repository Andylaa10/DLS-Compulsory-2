using EasyNetQ;
using MeasurementService.Core.Services.Interfaces;
using Messaging;
using Messaging.SharedMessages;

namespace MeasurementService.Core.Helper.MessageHandlers;

public class DeletePatientHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DeletePatientHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleDeletePatient(DeletePatientMessage message)
    {
        Console.WriteLine("joe");
        Console.WriteLine(message.Message);
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var measurementService = scope.ServiceProvider.GetRequiredService<IMeasurementService>();
            await measurementService.DeleteMeasurementsOnPatient(message.SSN);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("Something went wrong");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running..");
        Console.WriteLine("ANDY");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
        );

        const string topic = "DeletePatient";
        
        await messageClient.Listen<DeletePatientMessage>(HandleDeletePatient, topic);
    }
}