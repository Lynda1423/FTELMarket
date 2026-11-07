namespace FTELMarket.Services;

public class FlakyService(HttpClient client, IHttpClientFactory factory)
{
    public async Task<string> GetFlakyDataAsync()
    {
        // Option 1 : Utilisation du Typed Client (client)
        var responseTyped = await client.GetAsync("/api/flaky");

        // Option 2 : Utilisation du Named Client (factory)
        var namedClient = factory.CreateClient("Flaky");
        var responseNamed = await namedClient.GetAsync("/api/flaky");

        return $"Typed: {responseTyped.StatusCode}, Named: {responseNamed.StatusCode}";
    }
}