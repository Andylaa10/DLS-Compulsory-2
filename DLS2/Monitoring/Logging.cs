using Serilog;
using Serilog.Enrichers.Span;

namespace Monitoring;

public static class Logging
{
    public static ILogger Log => Serilog.Log.Logger;
    
    static Logging()
    {
        // Serilog
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Seq("http://seq:5345")
            .WriteTo.Console()
            .Enrich.WithSpan()
            .CreateLogger();
    }
}