using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.DTOs;

public class Registerdto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 8)]
    public string Password { get; set; }
    
}