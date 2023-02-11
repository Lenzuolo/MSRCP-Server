namespace MSRCP_Server.Models;

public class QR
{
    public int Id { get; set; }

    public string Code { get; set; }

    public DateOnly ValidDate { get; set; }
}
