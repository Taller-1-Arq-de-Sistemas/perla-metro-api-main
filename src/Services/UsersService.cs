using PerlaMetroApiMain.DTOs.Users;
using PerlaMetroApiMain.DTOs.Auth;
using PerlaMetroApiMain.Exceptions;
using PerlaMetroApiMain.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace PerlaMetroApiMain.Services;

public class UsersService : IUsersService
{
    private readonly string _serviceUrl;
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions SerializerOptions = new(JsonSerializerDefaults.Web);

    public UsersService(IConfiguration configuration, HttpClient httpClient)
    {
        _serviceUrl = configuration.GetValue<string>("USERS_SERVICE_URL") ?? "http://localhost:3000";
        _httpClient = httpClient;
    }

    public async Task<RegisterPassengerResponseDto> Register(RegisterPassengerRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/auth/register", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<RegisterPassengerResponseDto>(payload);
    }

    public async Task<LoginUserResponseDto> Login(LoginUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/auth/login", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<LoginUserResponseDto>(payload);
    }

    public async Task<GetUserResponseDto> Create(CreateUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PostAsync($"{_serviceUrl}/users", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<GetUserResponseDto>(payload);
    }

    public async Task Update(string userId, EditUserRequestDto user, CancellationToken ct)
    {
        var userData = JsonSerializer.Serialize(user);
        var response = await _httpClient.PatchAsync($"{_serviceUrl}/users/{userId}", new StringContent(userData, Encoding.UTF8, "application/json"), ct);
        await ReadContentOrThrow(response, ct);
    }
    public async Task SoftDelete(string userId, CancellationToken ct)
    {
        var response = await _httpClient.DeleteAsync($"{_serviceUrl}/users/{userId}", ct);
        await ReadContentOrThrow(response, ct);
    }

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

    public async Task<GetUserResponseDto> GetById(string userId, CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"{_serviceUrl}/users/{userId}", ct);
        var payload = await ReadContentOrThrow(response, ct);
        return DeserializeOrThrow<GetUserResponseDto>(payload);
    }


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
