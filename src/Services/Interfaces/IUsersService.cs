using PerlaMetroApiMain.DTOs.Auth;
using PerlaMetroApiMain.DTOs.Users;

namespace PerlaMetroApiMain.Services.Interfaces;

/// <summary>
/// Interface for Users Service
/// </summary>
public interface IUsersService
{
    Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct);
    Task<LoginUserResponseDto> Login(LoginUserRequestDto user, CancellationToken ct);
    Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct);
    Task Update(string userId, EditUserRequestDto user, CancellationToken ct);
    Task SoftDelete(string userId, CancellationToken ct);
    Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct);
    Task<GetUserResponseDto> GetById(string userId, CancellationToken ct);
}