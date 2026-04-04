using System.ComponentModel.DataAnnotations;

namespace SportsStatistics.Web.Pages.Identity.Signin.Models;

internal sealed class SigninInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me?")]
    public bool IsPersistant { get; set; } = false;
}
