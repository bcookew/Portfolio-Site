using PortfolioSite.Host.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PortfolioSite.Host.Services;

internal class Web3FormsService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<Web3FormsService> _logger;
    private readonly string _apiKey;

    public Web3FormsService(IHttpClientFactory clientFactory, IConfiguration configuration, ILogger<Web3FormsService> logger)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _logger = logger;

        _apiKey = _configuration["Web3FormsApiKey"] ?? throw new Exception("Could not recall Web3FormsApiKey!");
    }

    internal async Task<bool> SendFormAsync(ContactFormModel contactFormModel)
    {
        var formData = new Web3FormsRequest(_apiKey, contactFormModel);
        var serialized = JsonSerializer.Serialize(formData, new JsonSerializerOptions() { IncludeFields = true });

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.web3forms.com/submit");
        request.Headers.Add("Accept", "application/json");
        request.Content = new StringContent(serialized, new MediaTypeHeaderValue("application/json"));

        HttpClient client = _clientFactory.CreateClient();
        HttpResponseMessage response = await client.SendAsync(request);

        _logger.Log(response.IsSuccessStatusCode ? LogLevel.Information : LogLevel.Error, new ContactFormLog(formData, response).ToString());

        return response.IsSuccessStatusCode;
    }
}
