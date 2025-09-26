using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_api_main.src.DTOs.Station;

namespace perla_metro_api_main.src.Services.Interfaces
{
    public interface IStationService
    {
        public Task<CreateEditStationResponseDto> CreateStation(CreateStationDto request, CancellationToken ct);

        public Task<GetStationResponseDto> GetSations(string? Name, string? Type, bool? State, CancellationToken ct);

        public Task<GetByIdStationResponseDto> GetStationById(Guid ID, CancellationToken ct);

        public Task<CreateEditStationResponseDto> EditStation(Guid ID, EditStationDto request, CancellationToken ct);
    }
}