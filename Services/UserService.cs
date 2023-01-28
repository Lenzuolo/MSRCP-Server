using Microsoft.AspNetCore.Mvc;
using MSRCP_Server.DTO;
using MSRCP_Server.Models;
using MSRCP_Server.Repos;

namespace MSRCP_Server.Services;

public class UserService : IUserService
{
    private readonly IUserRepo UserRepo;
    public UserService(IUserRepo userRepo)
    {
        this.UserRepo = userRepo;
    }

    public async Task<IActionResult> CreateUserAsync(User user)
    {
        if (user == null)
        {
            return new JsonResult(new ExceptionDTO { Message = "User data is missing" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
        }

        var result = await UserRepo.AddAsync(user);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while creating new user" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
    }

    public async Task<IActionResult> GetTeamAsync(int id)
    {
        var result = await UserRepo.GetTeamAsync(id);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while getting team data" }) { StatusCode = StatusCodes.Status404NotFound};
    }

    public async Task<IActionResult> GetUserAsync(int id)
    {
        var result = await UserRepo.GetAsync(id);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured while getting user data"}) { StatusCode = StatusCodes.Status404NotFound}
    }

    public async Task<IActionResult> GetUserHistoryAsync(int id)
    {
        var result = await UserRepo.GetUserHistoryAsync(id);
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

    public async Task<IActionResult> UpdateUserAsync(User user)
    {
        if (user == null)
        {
            return new JsonResult(new ExceptionDTO { Message = "User data is missing" }) { StatusCode = StatusCodes.Status422UnprocessableEntity };
        }
        var result = await UserRepo.UpdateAsync(user);
        if (result != null)
        {
            return new JsonResult(result) { StatusCode = StatusCodes.Status200OK };
        }
        return new JsonResult(new ExceptionDTO { Message = "Problem occured updating user data"}) { StatusCode = StatusCodes.Status404NotFound };
    }
}
