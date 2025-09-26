using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using perla_metro_api_main.src.DTOs.Station;
using perla_metro_api_main.src.Services.Interfaces;
using PerlaMetroApiMain.Controllers;

namespace perla_metro_api_main.src.Controllers
{
    public class StationController : BaseApiController
    {
        private readonly IStationService _stationService;

        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        //TODO: agregar restriccion de roles
        [HttpPost("CreateStation")]
        public async Task<IActionResult> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            var result = await HelperStationController( () => _stationService.CreateStation(request, ct));
            return result;
        }

        //TODO: agregar restriccion de roles
        [HttpGet("Stations")]
        public async Task<IActionResult> GetStations([FromQuery] string? Name, [FromQuery] string? Type, [FromQuery] bool? State, CancellationToken ct)
        {
            var result = await HelperStationController( () =>  _stationService.GetSations(Name, Type, State, ct));
            return result;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetStationsById(Guid ID, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.GetStationById(ID,ct));
            return result;
        }

        //TODO: agregar restriccion de roles
        [HttpPut("EditStation/{ID}")]
        public async Task<IActionResult> EditStation(Guid ID, EditStationDto request, CancellationToken ct)
        {
            var result = await HelperStationController( () =>  _stationService.EditStation(ID,request,ct));
            return result;
        }

        //TODO: agregar restriccion de roles
        [HttpPut("ChangeStateStation/{ID}")]
        public async Task<IActionResult> DisabledEnabledStation(Guid ID, CancellationToken ct)
        {
            var result = await HelperStationController(() => _stationService.DisabledEnabledStation(ID,ct));
            return result;
        }
        

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