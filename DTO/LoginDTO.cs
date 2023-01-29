using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)] 
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string PasswordHash { get; set; }
}
