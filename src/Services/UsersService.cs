using PerlaMetroApiMain.DTOs.Users;
using PerlaMetroApiMain.DTOs.Auth;
using PerlaMetroApiMain.Exceptions;
using PerlaMetroApiMain.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PerlaMetroApiMain.Services;

/// <summary>
/// Service for user-related operations.
/// </summary>
public class UsersService : IUsersService
{
    private readonly string _serviceUrl;
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Constructor for UsersService.
    /// </summary>
    /// <param name="configuration">Configuration to get the users service URL.</param>
    /// <param name="httpClient">The HTTP client to make requests.</param>
    public UsersService(IConfiguration configuration, HttpClient httpClient)
    {
        _serviceUrl = configuration.GetValue<string>("USERS_SERVICE_URL") ?? "http://localhost:3000";
        _httpClient = httpClient;
    }

    /// <inheritdoc />
    public async Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/auth/register", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<RegisterPassengerResponseDto>(payload);
    }

    /// <inheritdoc />
    public async Task<LoginUserResponseDto> Login(LoginUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/auth/login", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<LoginUserResponseDto>(payload);
    }

    /// <inheritdoc />
    public async Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/users", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<GetUserResponseDto>(payload);
    }

    /// <inheritdoc />
    public async Task Update(string userId, EditUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PatchAsync($"{_serviceUrl}/users/{userId}", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        await ReadContentOrThrow(response, ct);
    }

    /// <inheritdoc />
    public async Task SoftDelete(string userId, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"{_serviceUrl}/users/{userId}", ct);
        await ReadContentOrThrow(response, ct);
    }

    /// <inheritdoc />
    public async Task<List<ListUserResponseDto>> GetAll(string? name, string? email, string? status, CancellationToken ct)
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(name)) query.Add($"name={Uri.EscapeDataString(name)}");
        if (!string.IsNullOrEmpty(email)) query.Add($"email={Uri.EscapeDataString(email)}");
        if (!string.IsNullOrEmpty(status)) query.Add($"status={Uri.EscapeDataString(status)}");
        var response = await _httpClient.GetAsync($"{_serviceUrl}/users?{string.Join("&", query)}", ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<List<ListUserResponseDto>>(payload);
    }

    /// <inheritdoc />
    public async Task<GetUserResponseDto> GetById(string userId, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"{_serviceUrl}/users/{userId}", ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<GetUserResponseDto>(payload);
    }

    /// <summary>
    /// Reads the content of an HTTP response or throws an exception if the response indicates an error.
    /// </summary>
    /// <param name="response">The HTTP response message.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The response content as a string.</returns>
    /// <exception cref="DownstreamServiceException"></exception>
    private static async Task<string> ReadContentOrThrow(HttpResponseMessage response, CancellationToken ct)
    {
        var content = response.Content is null
            ? string.Empty
            : await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            var contentType = response.Content?.Headers.ContentType?.ToString();
            throw new DownstreamServiceException((int)response.StatusCode, content, contentType);
        }

        return content;
    }

    /// <summary>
    /// Deserializes JSON payload to the specified type or throws an exception if deserialization fails.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="payload">The JSON payload.</param>
    /// <returns>The deserialized object of type T.</returns>
    /// <exception cref="InternalErrorException"></exception>
    private static T DeserializeOrThrow<T>(string payload)
    {
        var result = JsonSerializer.Deserialize<T>(payload, SerializerOptions);
        if (result is null)
        {
            throw new InternalErrorException($"Failed to deserialize response to {typeof(T).Name}.");
        }

        return result;
    }
}
