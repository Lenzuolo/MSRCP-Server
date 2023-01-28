using Microsoft.AspNetCore.Mvc;
using MSRCP_Server.Models;

namespace MSRCP_Server.Services;

public interface IUserService
{
    Task<IActionResult> CreateUserAsync(User user);
    Task<IActionResult> GetUserAsync(int id);
    Task<IActionResult> UpdateUserAsync(User user);
    Task<IActionResult> GetTeamAsync(int id);
    Task<IActionResult> GetUserHistoryAsync(int id);
    Task<IActionResult> ScanQRCodeAsync(User user, string code);
}
