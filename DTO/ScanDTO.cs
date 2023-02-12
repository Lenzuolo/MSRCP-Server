using MSRCP_Server.Models;
using System.ComponentModel.DataAnnotations;

namespace MSRCP_Server.DTO
{
    public class ScanDTO
    {
        [Required(ErrorMessage ="User id is missing")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "QR Code is required")]
        public string Code { get; set; }
        
    }
}
