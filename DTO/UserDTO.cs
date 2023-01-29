using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO;

public class UserDTO
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required", AllowEmptyStrings = false)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required", AllowEmptyStrings = false)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string PasswordHash { get; set; }

    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    public string UserName { get; set; }

    public bool IsAdmin { get; set; }
}
