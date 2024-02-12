using System.ComponentModel.DataAnnotations;

namespace PortfolioSite.Host.Models;

public class ContactFormModel
{
    [Required(ErrorMessage = "Required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage="Enter a valid eMail Address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Required")]
    [MinLength(50, ErrorMessage = "Below 50 character threshold")]
    [MaxLength(500, ErrorMessage = "Exceeds 500 character limit")]
    public string Message { get; set; } = string.Empty;
}
