namespace MSRCP_Server.Models;

public class User
{
    public Guid UserGuid { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public ICollection<WorkData> WorkDatas { get; set; }
}
