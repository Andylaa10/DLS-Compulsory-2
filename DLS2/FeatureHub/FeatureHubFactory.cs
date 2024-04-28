namespace FeatureHub;

public class FeatureHubFactory
{
    public static FeatureHubClient CreateFeatureHub()
    {
        const string apiKey = "ae1cb182-5012-4082-a86d-a214bdd0956c/c8se2E7n6MFGLoakrYMfwbQ7yRECbyRRlCFoCdbd";
        return new FeatureHubClient(apiKey);
    }
}