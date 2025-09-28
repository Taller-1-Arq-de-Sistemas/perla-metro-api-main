namespace PerlaMetroApiMain.DTOs.Users;

/// <summary>
/// DTO for editing an existing user.
/// </summary>
public sealed class EditUserRequestDto
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
}
