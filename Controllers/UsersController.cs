using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSRCP_Server.DTO;
using MSRCP_Server.Services;
using MSRCP_Server.Models;

namespace MSRCP_Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(User),StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDTO loginDTO) => await userService.Login(loginDTO.UserName, loginDTO.PasswordHash);

    [HttpPost]
    [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateAsync([FromBody] RegisterDTO registerDTO) => await userService.CreateUserAsync(registerDTO);

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromQuery] int id) => await userService.GetUserAsync(id);

    [HttpGet("history/{id:int}")]
    [ProducesResponseType(typeof(ICollection<WorkData>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserHistoryAsync([FromQuery] int id) => await userService.GetUserHistoryAsync(id);

    [HttpGet("team/{id:int}")]
    [ProducesResponseType(typeof(ICollection<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTeamAsync([FromQuery] int id) => await userService.GetTeamAsync(id);

    [HttpPatch]
    [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SerializableError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ExceptionDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserDTO user) => await userService.UpdateUserAsync(user);


}
