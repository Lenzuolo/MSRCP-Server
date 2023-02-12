using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using MSRCP_Server.DTO;
using MSRCP_Server.Models;
using MSRCP_Server.Repos;

namespace MSRCP_Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepo userRepo;
    public UserService(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<IActionResult> Login(string userName, string password)
    {
        var result = await userRepo.GetAsync(userName);
        if (result != null && BCrypt.Net.BCrypt.Verify(password, result.PasswordHash))
        {
            result.PasswordHash = "";
            return new JsonResult(result) { StatusCode = StatusCodes.Status202Accepted };
        }
        return new JsonResult(new ExceptionDTO { Message = "User not found" }) { StatusCode = StatusCodes.Status404NotFound };
    }
    public async Task<IActionResult> CreateUserAsync(RegisterDTO registerDTO)
    {
        var user = CreateUserFromRegisterDTO(registerDTO);

        var result = await userRepo.AddAsync(user);
        if (result != null)
        {
            result.PasswordHash = "";
            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while creating new user" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
    }

    public async Task<IActionResult> GetTeamAsync(int id)
    {
        var result = await userRepo.GetTeamAsync(id);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while getting team data" }) { StatusCode = StatusCodes.Status404NotFound};
    }

    public async Task<IActionResult> GetUserAsync(int id)
    {
        var result = await userRepo.GetAsync(id);
        if (result != null)
        {
            result.PasswordHash = "";
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while getting user data" }) { StatusCode = StatusCodes.Status404NotFound };
    }

    public async Task<IActionResult> GetUserHistoryAsync(int id)
    {
        var result = await userRepo.GetUserHistoryAsync(id);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while getting user history logs" }) { StatusCode = StatusCodes.Status404NotFound };
    }

    public async Task<IActionResult> ScanQRCodeAsync(int userId, string code)
    {
        var result = await userRepo.ScanCodeAsync(userId, code);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Invalid code scanned or user not found"}) { StatusCode = StatusCodes.Status404NotFound };
    }

    public async Task<IActionResult> GenerateQRAsync()
    {
        QR result;
        var random = new Random();
        while (true)
        {
            var code = $"MSRCP-{random.Next()}";
            if (!userRepo.CodeAlreadyExists(code))
            {
                result = await userRepo.GenerateQRAsync(new QR() { Code = code, ValidDate = DateOnly.FromDateTime(DateTime.Now) });
                break;
            }
        }
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        } else
        {
            return new JsonResult(new ExceptionDTO { Message = "Error generating code" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
        }
    }

    public async Task<IActionResult> UpdateUserAsync(UpdateUserDTO userDTO)
    {
        var user = await GetUserFromUpdateUserDTO(userDTO);
        if (user != null)
        {
            var result = await userRepo.UpdateAsync(user);
            if (result != null)
            {
                return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
            }
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured updating user data"}) { StatusCode = StatusCodes.Status404NotFound };
    }

    private User CreateUserFromRegisterDTO(RegisterDTO registerDTO)
    {
        return new User
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
            UserName = (registerDTO.FirstName.ElementAt(0) + registerDTO.LastName).ToLower(),
            IsAdmin = registerDTO.IsAdmin
        };
    }

    private async Task<User> GetUserFromUpdateUserDTO(UpdateUserDTO userDTO)
    {
        var user = await userRepo.GetAsync(userDTO.Id);
        if (user != null)
        {
            user.UserName = userDTO.UserName;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
            user.IsAdmin = userDTO.IsAdmin;
        }
        return user;
    }

}
