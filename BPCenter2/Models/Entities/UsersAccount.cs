using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPCenter2.Models.Entities;

[Table("UsersAccount")]
public class UsersAccount
{
    [Column("userNames")]
    [MaxLength(50)]
    public string? userNames { get; set; } = string.Empty;

    [Column("userLastNames")]
    [MaxLength(50)]
    public string? userLastNames { get; set; } = string.Empty;

    [Key]
    [Column("userEmail")]
    [MaxLength(50)]
    public string? userEmail { get; set; } = string.Empty;

    [Column("userPassword")]
    [MaxLength(300)]
    public string? userPassword { get; set; } = string.Empty;

    [Column("userRole")]
    [MaxLength(20)]
    public string? userRole { get; set; } = string.Empty;
}
