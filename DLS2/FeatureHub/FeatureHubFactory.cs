namespace FeatureHub;

public class FeatureHubFactory
{
    public static FeatureHubClient CreateFeatureHub()
    {
        const string apiKey = "6bba3e9c-faa1-4e85-b6a2-8bfb15ee25f1/B1vtNAIwHBFSpVE2W3yPAYfNiY3zmaEOyNhETPDk";
        return new FeatureHubClient(apiKey);
    }
}