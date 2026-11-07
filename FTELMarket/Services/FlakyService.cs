namespace FTELMarket.Services;

public class FlakyService(HttpClient client, IHttpClientFactory factory)
{
    public void Sample()
    {
        var option1BaseAddress = client.BaseAddress;
        var option2BaseAddress = factory.CreateClient("Flaky").BaseAddress;
    }
}

