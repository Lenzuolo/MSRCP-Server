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

    public async Task<IActionResult> Login(string userName, string passwordHash)
    {
        var result = await userRepo.Login(userName, passwordHash);
        if (result != null)
        {
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

    public Task<IActionResult> ScanQRCodeAsync(User user, string code)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> UpdateUserAsync(UserDTO userDTO)
    {
        var user = CreateUserFromUserDTO(userDTO);

        var result = await userRepo.UpdateAsync(user);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured updating user data"}) { StatusCode = StatusCodes.Status404NotFound };
    }

    private User CreateUserFromRegisterDTO(RegisterDTO registerDTO)
    {
        return new User
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            PasswordHash = registerDTO.PasswordHash,
            UserName = (registerDTO.FirstName.ElementAt(0) + registerDTO.LastName).ToLower(),
            IsAdmin = registerDTO.IsAdmin
        };
    }

    private User CreateUserFromUserDTO(UserDTO userDTO)
    {
        return new User
        {
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            PasswordHash = userDTO.PasswordHash,
            UserName = userDTO.UserName,
            IsAdmin = userDTO.IsAdmin,
            Id = userDTO.Id
        };
    }

}
