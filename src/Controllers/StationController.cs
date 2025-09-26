using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using perla_metro_api_main.src.DTOs.Station;
using perla_metro_api_main.src.Services.Interfaces;
using PerlaMetroApiMain.Controllers;

namespace perla_metro_api_main.src.Controllers
{
    /// <summary>
    /// Controller responsible for station operations.
    /// Provides endpoints to create, retrieve, update, and enable/disable stations.
    /// </summary>
    public class StationController : BaseApiController
    {
        private readonly IStationService _stationService;

        /// <summary>
        /// Initializes a new instance of "StationController".
        /// </summary>
        /// <param name="stationService">Service handling the station operations logic.</param>
        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        /// <summary>
        /// Creates a new station.
        /// Requires admin role.
        /// </summary>
        /// <param name="request">Station data to create.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Response with created station details.</returns>
        [Authorize(Roles = "admin")]
        [HttpPost("CreateStation")]
        public async Task<IActionResult> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.CreateStation(request, ct));
            return result;
        }

        /// <summary>
        /// Retrieves a list of stations filtered by name, type, or state.
        /// Requires admin role.
        /// </summary>
        /// <param name="Name">Optional filter by station name.</param>
        /// <param name="Type">Optional filter by station type.</param>
        /// <param name="State">Optional filter by station state (Active/Inactive).</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>List of stations matching the filters.</returns>
        [Authorize(Roles = "admin")]
        [HttpGet("Stations")]
        public async Task<IActionResult> GetStations([FromQuery] string? Name, [FromQuery] string? Type, [FromQuery] bool? State, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.GetSations(Name, Type, State, ct));
            return result;
        }

        /// <summary>
        /// Retrieves a station by ID.
        /// </summary>
        /// <param name="ID">ID of the station.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Station details if found.</returns>
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetStationsById(Guid ID, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.GetStationById(ID, ct));
            return result;
        }

        /// <summary>
        /// Edits an existing station by ID.
        /// Requires admin role.
        /// </summary>
        /// <param name="ID">Unique identifier of the station to edit.</param>
        /// <param name="request">Station data to update.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Response with updated station details.</returns>
        [Authorize(Roles = "admin")]
        [HttpPut("EditStation/{ID}")]
        public async Task<IActionResult> EditStation(Guid ID, EditStationDto request, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.EditStation(ID, request, ct));
            return result;
        }

        /// <summary>
        /// Changes the state (Activa/Inactiva) of a station by ID.
        /// Requires admin role.
        /// </summary>
        /// <param name="ID">ID of the station.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Response indicating success or failure of the operation.</returns>
        [Authorize(Roles = "admin")]
        [HttpPut("ChangeStateStation/{ID}")]
        public async Task<IActionResult> DisabledEnabledStation(Guid ID, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.DisabledEnabledStation(ID, ct));
            return result;
        }

        /// <summary>
        /// Helper method to standardize service call handling in the controller.
        /// Manages exceptions and returns appropriate HTTP responses.
        /// </summary>
        /// <typeparam name="T">Type of the service response.</typeparam>
        /// <param name="serviceCall">Delegate representing the service call.</param>
        /// <returns>HTTP response containing the service result or an error message.</returns>
        private async Task<IActionResult> HelperStationController<T>(Func<Task<T>> serviceCall)
        {
            try
            {
                var response = await serviceCall();
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}