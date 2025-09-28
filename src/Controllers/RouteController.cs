using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerlaMetroApiMain.DTOs.Route;
using PerlaMetroApiMain.Services;
using PerlaMetroApiMain.Services.Interfaces;
using PerlaMetroApiMain.Controllers;

namespace PerlaMetroApiMain.Controllers
{

    public class RouteController(IRouteService routeService) : BaseApiController
    {
        private readonly IRouteService _routeServices = routeService;


        [HttpGet("routeAll")]
        public async Task<IActionResult> GetRoutes(CancellationToken ct)
        {
            var result = await HelperStationController(() => _routeServices.GetRoutes(ct));
            return result;
        }

        [HttpPut("DisabledEnabled/{id}")]
        public async Task<IActionResult> DisabledEnabledRoute([FromRoute] int id, CancellationToken ct)
        {
            var result = await HelperStationController(() => _routeServices.DisabledEnabledRoute(id, ct));
            return result;
        }
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> updateDataRoute([FromRoute] int id, [FromBody] updateDataRoute request, CancellationToken ct)
        {
            var result = await HelperStationController(() => _routeServices.EditRoute(id, request, ct));
            return result;
        }

        [HttpGet("unic/{id}")]
        public async Task<IActionResult> getRouteId([FromRoute] int id, CancellationToken ct)
        {
            var result = await HelperStationController(() => _routeServices.GetRouteById(id, ct));
            return result;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> createRoute([FromBody] CreateRoute request, CancellationToken ct)
        {
            var result = await HelperStationController(() => _routeServices.CreateRoute(request, ct));
            return result;
        }
        protected async Task<IActionResult> HelperStationController<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action();

                if (result == null)
                    return NotFound(new { message = "No se encontraron resultados" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Aquí puedes loggear el error con ILogger
                return StatusCode(500, new { message = "Error interno del servidor", detail = ex.Message });
            }
        }




    }

}