using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PerlaMetroApiMain.DTOs.Route;
using PerlaMetroApiMain.Services.Interfaces;

namespace PerlaMetroApiMain.Services
{
    public class RouteService : IRouteService
    {
        private readonly string? _routeUrl = null;
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);

        public RouteService(IConfiguration configuration, HttpClient httpClient)
        {
            _routeUrl = configuration.GetValue<string>("ROUTESERVICESURL") ?? "http://localhost:3000/";
            _httpClient = httpClient;
        }
        public async Task<CreateRouteResponse> CreateRoute(CreateRoute request, CancellationToken ct)
        {
            var routeData = JsonSerializer.Serialize(request);

            var response = await _httpClient.PostAsync($"{_routeUrl}api/routeStation/create", new StringContent(routeData, Encoding.UTF8, "application/json"), ct);
            var payload = await response.Content.ReadAsStringAsync(ct);
            var result = JsonSerializer.Deserialize<CreateRouteResponse>(payload, serializerOptions) ?? throw new InvalidOperationException("Petición no valida");
            return result;
        }

        public async Task<DeleteRoute> DisabledEnabledRoute(int ID, CancellationToken ct)
        {

            var response = await _httpClient.PutAsync($"{_routeUrl}api/routeStation/delete/{ID}", new StringContent("", Encoding.UTF8, "application/json"), ct);

            var payload = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Error al llamar al servicio: {response.StatusCode}, {payload}");
            }

            var result = JsonSerializer.Deserialize<DeleteRoute>(payload, serializerOptions) ?? throw new InvalidOperationException("Petición no valida");
            return result;

        }

        public async Task<UpdateRoute> EditRoute(int ID, updateDataRoute request, CancellationToken ct)
        {

            var routeData = JsonSerializer.Serialize(request);

            var response = await _httpClient.PutAsync($"{_routeUrl}api/routeStation/update/{ID}", new StringContent(routeData, Encoding.UTF8, "application/json"), ct);
            var payload = await response.Content.ReadAsStringAsync(ct);
            var result = JsonSerializer.Deserialize<UpdateRoute>(payload, serializerOptions) ?? throw new InvalidOperationException("Petición no valida");
            return result;
        }

        public async Task<GetRoute> GetRoutes(CancellationToken ct)
        {

            var response = await _httpClient.GetAsync($"{_routeUrl}api/routeStation/all");


            var payload = await response.Content.ReadAsStringAsync(ct);
            var result = JsonSerializer.Deserialize<GetRoute>(payload, serializerOptions) ?? throw new InvalidOperationException("Petición no valida");
            return result;
        }

        public async Task<GetRoute> GetRouteById(int ID, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"{_routeUrl}api/routeStation/unic/{ID}");


            var payload = await response.Content.ReadAsStringAsync(ct);
            var result = JsonSerializer.Deserialize<GetRoute>(payload, serializerOptions) ?? throw new InvalidOperationException("Petición no valida");
            return result;
        }

    }




}