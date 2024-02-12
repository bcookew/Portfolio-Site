namespace PortfolioSite.Host.Models;

internal record Web3FormsRequest
{
    public string apikey { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Message { get; init; }

    public Web3FormsRequest(string apiKey, ContactFormModel contactFormModel)
    {
        apikey = apiKey;
        Name = contactFormModel.Name;
        Email = contactFormModel.Email;
        Message = contactFormModel.Message;
    }
}