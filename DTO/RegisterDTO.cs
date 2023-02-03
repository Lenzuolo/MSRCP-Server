using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First name is required", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required", AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
