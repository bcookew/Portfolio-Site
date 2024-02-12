using System.Text.Json;

namespace PortfolioSite.Host.Models;

internal class ContactFormLog
{
    Web3FormsRequest Form { get; set; }
    HttpResponseMessage Response { get; set; }

    internal ContactFormLog(Web3FormsRequest web3FormsRequest, HttpResponseMessage response)
    {
        Form = web3FormsRequest;
        Response = response;
    }

    public override string ToString()
    {
        string log;

        if (Response.IsSuccessStatusCode)
            log = JsonSerializer.Serialize(Form);
        else
            log = JsonSerializer.Serialize(new { Form, Response });

        return log;
    }
}
