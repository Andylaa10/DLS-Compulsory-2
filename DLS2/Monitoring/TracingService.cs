using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Monitoring;

public static class TracingService
{
    public static readonly string ServiceName = Assembly.GetCallingAssembly().GetName().Name ?? "UnknownService";
    public static TracerProvider TracerProvider;
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
    
    static TracingService()
    {
        TracerProvider = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
            .AddSource(ServiceName)
            .AddZipkinExporter(c=>c.Endpoint = new Uri("http://zipkin:9411/api/v2/spans"))
            .AddConsoleExporter()
            .Build();
    }
}