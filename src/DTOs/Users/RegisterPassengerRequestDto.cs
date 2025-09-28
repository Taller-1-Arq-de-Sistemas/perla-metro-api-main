namespace PerlaMetroApiMain.DTOs.Auth;

/// <summary>
/// DTO for registering a new passenger user.
/// </summary>
public sealed class RegisterPassengerRequestDto
{
    /// <summary>
    /// The first name of the passenger.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The last names of the passenger.
    /// </summary>
    public string LastNames { get; set; } = null!;

    /// <summary>
    /// The email address of the passenger.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// The password for the passenger.
    /// </summary>
    public string Password { get; set; } = null!;
}
