namespace PerlaMetroApiMain.DTOs.Users;

/// <summary>
/// DTO for querying users with optional filters.
/// </summary>
public sealed class ListUsersQueryDto
{
    /// <summary>
    /// The name filter for querying users.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The email filter for querying users.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// The status filter for querying users. Can be "active", "deleted", or "all".
    /// </summary>
    public string? Status { get; set; }
}
