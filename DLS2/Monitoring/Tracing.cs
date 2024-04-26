using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Monitoring;

public static class Tracing
{

    public static OpenTelemetryBuilder Setup(this OpenTelemetryBuilder builder)
    {
        string serviceName = Assembly.GetCallingAssembly().GetName().Name ?? "Unknown";

        return builder.WithTracing(tcb =>
        {
            tcb
                .AddSource(serviceName)
                .AddZipkinExporter(c => c.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter()
                .Build();
        });
    }
}