using System.ComponentModel.DataAnnotations.Schema;

namespace MSRCP_Server.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<WorkData> WorkDatas { get; set; }

    [NotMapped]
    public WorkData CurrentWorkData { get; set; }
}
