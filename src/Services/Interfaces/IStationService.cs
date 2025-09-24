using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_api_main.src.DTOs.Station;

namespace perla_metro_api_main.src.Services.Interfaces
{
    public interface IStationService
    {
        public Task<StationResponseGropuDto> CreateStation(CreateStationDto request, CancellationToken ct);
    }
}