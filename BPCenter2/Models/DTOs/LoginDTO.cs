using System.ComponentModel.DataAnnotations;

namespace BPCenter2.Models.DTOs;

public class LoginDTO
{
    public string? userFullName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide user email")]
    public string? userEmail { get; set; } = string.Empty;
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide user password")]
    public string? userPassword { get; set; } = string.Empty;
    public string? userRole { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
}
