using Microsoft.AspNetCore.Mvc;
using MSRCP_Server.DTO;
using MSRCP_Server.Models;

namespace MSRCP_Server.Services;

public interface IUserService
{
    Task<IActionResult> CreateUserAsync(RegisterDTO registerDTO);
    Task<IActionResult> GetUserAsync(int id);
    Task<IActionResult> GetUserIdAsync(string username);
    Task<IActionResult> UpdateUserAsync(UpdateUserDTO userDTO);
    Task<IActionResult> GetTeamAsync(int id);
    Task<IActionResult> GetUserHistoryAsync(int id);
    Task<IActionResult> ScanQRCodeAsync(int userId, string code);
    Task<IActionResult> Login(string userName, string passwordHash);
    Task<IActionResult> GenerateQRAsync();
}
