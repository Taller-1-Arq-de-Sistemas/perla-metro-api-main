using PerlaMetroApiMain.DTOs.Auth;
using PerlaMetroApiMain.DTOs.Users;

namespace PerlaMetroApiMain.Services.Interfaces;

/// <summary>
/// Interface for Users Service
/// </summary>
public interface IUsersService
{
    /// <summary>
    /// Registers a new passenger.
    /// </summary>
    /// <param name="user">The registration details of the passenger.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The registered passenger details.</returns>
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct);

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="user">The login details of the user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The logged-in user details.</returns>
    Task<LoginUserResponseDto> Login(LoginUserRequestDto user, CancellationToken ct);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The details of the user to create.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The created user details.</returns>
    Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="user">The updated user details.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Update(string userId, EditUserRequestDto user, CancellationToken ct);

    /// <summary>
    /// Soft deletes a user.
    /// </summary>
    /// <param name="userId">The ID of the user to soft delete.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SoftDelete(string userId, CancellationToken ct);

    /// <summary>
    /// Retrieves all users with optional filtering by name, email, and status.
    /// </summary>
    /// <param name="name">The name to filter users by (optional).</param>
    /// <param name="email">The email to filter users by (optional).</param>
    /// <param name="status">The status to filter users by (optional).</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A list of users matching the specified criteria.</returns>
    Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct);

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The details of the user.</returns>
    Task<GetUserResponseDto> GetById(string userId, CancellationToken ct);
}