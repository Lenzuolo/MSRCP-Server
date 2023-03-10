using MSRCP_Server.Models;

namespace MSRCP_Server.Repos;

public interface IUserRepo
{
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<User> GetAsync(int id);
    Task<int> GetUserIdAsync(string username);
    Task<ICollection<User>> GetTeamAsync(int adminId);
    Task<ICollection<WorkData>> GetUserHistoryAsync(int id);
    Task<WorkData> ScanCodeAsync(int userId, string code);
    Task<User> GetAsync(string username);
    Task<QR> GenerateQRAsync(QR qr);
    bool CodeAlreadyExists(string code);
}
