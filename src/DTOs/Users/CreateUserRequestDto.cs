namespace PerlaMetroApiMain.DTOs.Users;

/// <summary>
/// DTO for creating a new user.
/// </summary>
public sealed class CreateUserRequestDto
{
    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The last names of the user.
    /// </summary>
    public string LastNames { get; set; } = null!;

    /// <summary>
    /// The email address of the user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the user.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// The role of the user (either "admin" or "passenger").
    /// </summary>
    public string Role { get; set; } = "passenger";
}
