using System.ComponentModel.DataAnnotations;

namespace BPCenter2.Models.DTOs;

public class LoginDTO
{
    public string? userFullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Por favor, debe escribir un correo!..")]
    public string? userEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Por favor, debe escribir una contraseña!..")]
    public string? userPassword { get; set; } = string.Empty;
    public string? userRole { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
}
