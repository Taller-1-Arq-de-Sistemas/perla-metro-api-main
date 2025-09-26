using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using perla_metro_api_main.src.DTOs.Station;
using perla_metro_api_main.src.Services.Interfaces;

namespace perla_metro_api_main.src.Services
{
    public class StationService : IStationService
    {

        private readonly string _stationUrl = null!;

        private readonly HttpClient _httpclient;

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);


        public StationService(IConfiguration configuration, HttpClient httpClient)
        {
            _stationUrl = configuration.GetValue<string>("StationServiceUrl") ?? throw new InvalidOperationException("StationServiceUrl no esta configurado correctamente."); ;
            _httpclient = httpClient;
        }

        public async Task<CreateEditStationResponseDto> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            var stationData = JsonSerializer.Serialize(request);
            var response = await _httpclient.PostAsync($"{_stationUrl}/Station/CreateStation", new StringContent(stationData, Encoding.UTF8, "application/json"), ct);

            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al crear una estacion: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<CreateEditStationResponseDto>(resultResponse, options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return result;
        }

        public async Task<GetStationResponseDto> GetSations(string? Name, string? Type, bool? State, CancellationToken ct)
        {
            var queryParams = new Dictionary<string, string?>();

            if (!string.IsNullOrWhiteSpace(Name))
            {
                queryParams["name"] = Name;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                queryParams["type"] = Type;
            }

            if (State.HasValue)
            {
                queryParams["state"] = State.Value.ToString();
            }

            var url = QueryHelpers.AddQueryString($"{_stationUrl}/Station/Stations", queryParams);

            var response = await _httpclient.GetAsync(url, ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al obtener listado de estaciones: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<GetStationResponseDto>(resultResponse, options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return result;

        }


        public async Task<GetByIdStationResponseDto> GetStationById(Guid ID, CancellationToken ct)
        {
            var response = await _httpclient.GetAsync($"{_stationUrl}/Station/{ID}", ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al obtener estacion solicitada: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<GetByIdStationResponseDto>(resultResponse, options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return result;

        }

        public async Task<CreateEditStationResponseDto> EditStation(Guid ID, EditStationDto request, CancellationToken ct)
        {
            var EditData = JsonSerializer.Serialize(request);
            var response = await _httpclient.PutAsync($"{_stationUrl}/Station/EditStation/{ID}", new StringContent(EditData, Encoding.UTF8, "application/json"), ct);

            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al crear una estacion: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);


            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<CreateEditStationResponseDto>(resultResponse, options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return result;
        }


        public async Task<DisabledEnabledStationResponseDto> DisabledEnabledStation(Guid ID, CancellationToken ct)
        {
    
            var response = await _httpclient.PutAsync($"{_stationUrl}/Station/ChangeStateStation/{ID}",null,ct);

            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al crear una estacion: {response.StatusCode}, {errorContent}");
            }

            var resultResponse = await response.Content.ReadAsStringAsync(ct);


            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<DisabledEnabledStationResponseDto>(resultResponse,options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return result;
        }


    }
}