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

        [HttpPost("CreateStation")]
        public async Task<IActionResult> CreateStation(CreateStationDto request, CancellationToken ct)
        {
            try
            {
                var response = await _stationService.CreateStation(request, ct);
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