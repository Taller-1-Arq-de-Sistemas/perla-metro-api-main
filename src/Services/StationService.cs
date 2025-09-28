using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PerlaMetroApiMain.DTOs.Station;
using PerlaMetroApiMain.Services.Interfaces;

namespace PerlaMetroApiMain.Services
{
    /// <summary>
    /// Service implementation for station operations.  
    /// Handles communication with the external Station API to create, edit, retrieve, and enable/disable stations.
    /// </summary>
    public class StationService : IStationService
    {
        private readonly string _stationUrl = null!;
        private readonly HttpClient _httpclient;
        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);

        /// <summary>
        /// Initializes a new instance of "StationService".  
        /// Retrieves the Station service URL from the configuration and injects an "HttpClient".
        /// </summary>
        /// <param name="configuration">Application configuration containing Station service settings.</param>
        /// <param name="httpClient">HTTP client used to perform API requests.</param>
        /// <exception cref="InvalidOperationException">Thrown if StationServiceUrl is not properly configured.</exception>
        public StationService(IConfiguration configuration, HttpClient httpClient)
        {
            _stationUrl = configuration.GetValue<string>("StationServiceUrl")
                ?? throw new InvalidOperationException("StationServiceUrl is not configured properly.");
            _httpclient = httpClient;
        }

        /// <summary>
        /// Creates a new station in the system.
        /// </summary>
        /// <param name="request">DTO containing the station data to be created.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the created station information.</returns>
        /// <exception "HttpRequestException" Thrown if the API call fails.</exception>
        /// <exception "Exception" Thrown if the response cannot be deserialized.</exception>
        public async Task<CreateEditStationResponseDto> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            var stationData = JsonSerializer.Serialize(request);

            var response = await HelperStationService<CreateEditStationResponseDto>(
                () => _httpclient.PostAsync(
                    $"{_stationUrl}/Station/CreateStation",
                    new StringContent(stationData, Encoding.UTF8, "application/json"),
                    ct),
                ct);

            return response;
        }

        /// <summary>
        /// Get a list of stations filtered by optional parameters.
        /// </summary>
        /// <param name="Name">Optional filter for station name.</param>
        /// <param name="Type">Optional filter for station type (Origen, Destino, Intermedia).</param>
        /// <param name="State">Optional filter for station state (Activa/Inactiva).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a list of stations.</returns>
        /// <exception "HttpRequestException" Thrown if the API call fails.</exception>
        /// <exception "Exception" Thrown if the response cannot be deserialized.</exception>
        public async Task<GetStationResponseDto> GetSations(string? Name, string? Type, bool? State, CancellationToken ct)
        {
            var queryParams = new Dictionary<string, string?>();

            if (!string.IsNullOrWhiteSpace(Name))
                queryParams["name"] = Name;

            if (!string.IsNullOrWhiteSpace(Type))
                queryParams["type"] = Type;

            if (State.HasValue)
                queryParams["state"] = State.Value.ToString();

            var url = QueryHelpers.AddQueryString($"{_stationUrl}/Station/Stations", queryParams);

            var response = await HelperStationService<GetStationResponseDto>(
                () => _httpclient.GetAsync(url, ct), ct);

            return response;
        }

        /// <summary>
        /// Get a station by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the station details.</returns>
        /// <exception "HttpRequestException" Thrown if the API call fails.</exception>
        /// <exception "Exception" Thrown if the response cannot be deserialized.</exception>
        public async Task<GetByIdStationResponseDto> GetStationById(Guid ID, CancellationToken ct)
        {
            var response = await HelperStationService<GetByIdStationResponseDto>(
                () => _httpclient.GetAsync($"{_stationUrl}/Station/{ID}", ct), ct);

            return response;
        }

        /// <summary>
        /// Edits an existing station identified by its unique ID.
        /// </summary>
        /// <param name="ID">The unique identifier of the station (UUID v4).</param>
        /// <param name="request">DTO containing the updated station data.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the updated station information.</returns>
        /// <exception cref="HttpRequestException">Thrown if the API call fails.</exception>
        /// <exception cref="Exception">Thrown if the response cannot be deserialized.</exception>
        public async Task<CreateEditStationResponseDto> EditStation(Guid ID, EditStationDto request, CancellationToken ct)
        {
            var EditData = JsonSerializer.Serialize(request);

            var response = await HelperStationService<CreateEditStationResponseDto>(
                () => _httpclient.PutAsync(
                    $"{_stationUrl}/Station/EditStation/{ID}",
                    new StringContent(EditData, Encoding.UTF8, "application/json"),
                    ct),
                ct);

            return response;
        }

        /// <summary>
        /// Enables or disables a station identified by  ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a message about the operation result.</returns>
        /// <exception "HttpRequestException" Thrown if the API call fails.</exception>
        /// <exception "Exception" Thrown if the response cannot be deserialized.</exception>
        public async Task<DisabledEnabledStationResponseDto> DisabledEnabledStation(Guid ID, CancellationToken ct)
        {
            var response = await HelperStationService<DisabledEnabledStationResponseDto>(
                () => _httpclient.PutAsync($"{_stationUrl}/Station/ChangeStateStation/{ID}", null, ct),
                ct);

            return response;
        }

        /// <summary>
        /// Helper method to handle HTTP requests to the Station API.  
        /// Manages response validation, error handling, and deserialization of results.
        /// </summary>
        /// <typeparam name="T">The type of the response DTO expected from the API.</typeparam>
        /// <param name="serviceCall">The function representing the HTTP request to execute.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>The deserialized response of type <typeparamref name="T"/>.</returns>
        /// <exception  "HttpRequestException" Thrown when the API response is not successful.</exception>
        /// <exception  "Exception" Thrown when the response cannot be deserialized.</exception>
        private async Task<T> HelperStationService<T>(Func<Task<HttpResponseMessage>> serviceCall, CancellationToken ct)
        {
            var response = await serviceCall();

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error while processing station request: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<T>(resultResponse, options)
                ?? throw new Exception("Failed to deserialize the response.");

            return result;
        }
    }
}