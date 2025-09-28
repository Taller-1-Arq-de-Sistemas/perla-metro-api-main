namespace PerlaMetroApiMain.DTOs.Auth;

/// <summary>
/// DTO for user login request.
/// </summary>
public sealed class LoginUserRequestDto
{
    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the user.
    /// </summary>
    public string Password { get; set; } = null!;
}
