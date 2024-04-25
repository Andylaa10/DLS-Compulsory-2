using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Monitoring;

public static class Tracing
{
    public static OpenTelemetryBuilder Setup(this OpenTelemetryBuilder builder)
    {
        var serviceName = "MyTracer";
        var serviceVersion = "1.0.0";

        return builder.WithTracing(tcb =>
        {
            tcb
                .AddSource(serviceName)
                .AddZipkinExporter(c => c.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
                .AddConsoleExporter()
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter();
        });
    }
}