using MSRCP_Server.Models;
using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO
{
    public class ScanDTO
    {
        [Required(ErrorMessage ="User is missing")]
        public User User { get; set; }

        [Required(ErrorMessage = "QR Code is required")]
        public string Code { get; set; }
        
    }
}
