using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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

        public async Task<StationResponseGropuDto> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            var stationData = JsonSerializer.Serialize(request);
            var response = await _httpclient.PostAsync($"{_stationUrl}/Station/CreateStation", new StringContent(stationData, Encoding.UTF8, "application/json"), ct);

            if (!response.IsSuccessStatusCode)
            {

                var errorContent = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al crear una estacion: {response.StatusCode}, {errorContent}");
            }

            var result = await response.Content.ReadAsStringAsync(ct);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var wrapper = JsonSerializer.Deserialize<StationResponseGropuDto>(result, options) ?? throw new Exception("No se pudo deserializar la respuesta");

            return wrapper;
        }

    }
}