using System.ComponentModel.DataAnnotations;

namespace Company.Identity.Infrastructure.Auth.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required(ErrorMessage = "JWT signing key is required.")]
    [MinLength(32, ErrorMessage = "JWT key must be at least 32 characters for security.")]
    public string Key { get; set; } = null!;

    [Required(ErrorMessage = "JWT issuer is required.")]
    [Url(ErrorMessage = "Issuer must be a valid URL.")]
    public string Issuer { get; set; } = null!;

    [Required(ErrorMessage = "JWT audience is required.")]
    [Url(ErrorMessage = "Audience must be a valid URL.")]
    public string Audience { get; set; } = null!;
}