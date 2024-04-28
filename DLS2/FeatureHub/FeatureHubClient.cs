using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

namespace FeatureHub;
public class FeatureHubClient
{
    private readonly IFeatureHubConfig _config;

    public FeatureHubClient(string apiKey)
    {
        FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s);
        FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s);
        FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s);
        FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s);

        _config = new EdgeFeatureHubConfig("http://featurehub:8085",
            apiKey);
    }

    public async Task<bool> IsCountryAllowed(string country)
    {
        StrategyAttributeCountryName countryName;
    
        if (!Enum.TryParse(country, true, out countryName)) throw new ArgumentException("Could not find country");
        
        var featureToggle = await _config.NewContext().Country(countryName).Build();
        
        //Change to real feature 
        return featureToggle["DDO"].IsEnabled;
    }
   
}