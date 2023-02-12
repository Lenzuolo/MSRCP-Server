using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO;

public class UpdateUserDTO
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
    public string UserName { get; set; }

    public bool IsAdmin { get; set; }
}
