using Microsoft.AspNetCore.Mvc;
using PerlaMetroApiMain.DTOs.Users;
using PerlaMetroApiMain.DTOs.Auth;
using PerlaMetroApiMain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PerlaMetroApiMain.Controllers;

/// <summary>
/// Controller for user-related operations.
/// </summary>
public class UsersController : BaseApiController
{
    private readonly IUsersService _usersService;

    /// <summary>
    /// Constructor for UsersController.
    /// </summary>
    /// <param name="usersService">The users service.</param>
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    /// <summary>
    /// Registers a new passenger and returns their details along with an authentication token.
    /// </summary>
    /// <param name="user">The registration details of the passenger.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The details of the registered passenger along with an authentication token.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterPassengerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] RegisterPassengerRequestDto user, CancellationToken ct)
    {
        var createdUser = await _usersService.Register(user, ct);
        return CreatedAtAction(actionName: nameof(GetUserById),
                               controllerName: "Users",
                               routeValues: new { id = createdUser.Id },
                               value: createdUser);
    }

    /// <summary>
    /// Logs in a user and returns an authentication token.
    /// </summary>
    /// <param name="request">The login request details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The login response containing user details and token.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request, CancellationToken ct)
    {
        var response = await _usersService.Login(request, ct);
        return Ok(response);
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user creation details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The created user profile.</returns>
    [HttpPost]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto user, CancellationToken ct)
    {
        var createdUser = await _usersService.Create(user, ct);
        return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }

    /// <summary>
    /// Returns all users, with optional filtering by name, email, and status.
    /// </summary>
    /// <param name="query">The query parameters for filtering users.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A list of users matching the query.</returns>
    [HttpGet]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(List<ListUserResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers([FromQuery] ListUsersQueryDto query, CancellationToken ct)
    {
        var users = await _usersService.GetAll(query.Name, query.Email, query.Status, ct);
        return Ok(users);
    }

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The user profile if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(GetUserResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string id, CancellationToken ct)
    {
        var user = await _usersService.GetById(id, ct);
        return Ok(user);
    }

    /// <summary>
    /// Updates an existing user's details.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="user">The updated user details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A 204 No Content response if the update was successful; otherwise, an error response.</returns>
    [HttpPatch("{id}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserRequestDto user, CancellationToken ct)
    {
        await _usersService.Update(id, user, ct);
        return NoContent();
    }

    /// <summary>
    /// Soft deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A 204 No Content response if the deletion was successful; otherwise, an error response.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string id, CancellationToken ct)
    {
        await _usersService.SoftDelete(id, ct);
        return NoContent();
    }
}
