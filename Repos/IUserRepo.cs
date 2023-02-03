using MSRCP_Server.Models;

namespace MSRCP_Server.Repos;

public interface IUserRepo
{
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> GetAsync(int id);
    Task<ICollection<User>> GetTeamAsync(int adminId);
    Task<ICollection<WorkData>> GetUserHistoryAsync(int id);
    Task<WorkData> ScanCodeAsync(User user, string code);
    Task<User> GetUserAsync(string username);
}
