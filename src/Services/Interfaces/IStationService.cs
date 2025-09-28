using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using perla_metro_api_main.src.DTOs.Station;

namespace perla_metro_api_main.src.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Station service operations.  
    /// Provides methods to create, edit, retrieve, and enable/disable stations.
    /// </summary>
    public interface IStationService
    {
        /// <summary>
        /// Creates a new station in the system.
        /// </summary>
        /// <param name="request">DTO containing the station data to be created.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the created station information.</returns>
        Task<CreateEditStationResponseDto> CreateStation(CreateStationDto request, CancellationToken ct);

        /// <summary>
        /// Retrieves a list of stations filtered by optional parameters.
        /// </summary>
        /// <param name="Name">Optional filter for station name.</param>
        /// <param name="Type">Optional filter for station type (Origen, Destino, Intermedia).</param>
        /// <param name="State">Optional filter for station state (Activa/Inactiva).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a list of stations.</returns>
        Task<GetStationResponseDto> GetSations(string? Name, string? Type, bool? State, CancellationToken ct);

        /// <summary>
        /// Retrieves a station by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the station details.</returns>
        Task<GetByIdStationResponseDto> GetStationById(Guid ID, CancellationToken ct);

        /// <summary>
        /// Edits an existing station identified by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="request">DTO containing the updated station data.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the updated station information.</returns>
        Task<CreateEditStationResponseDto> EditStation(Guid ID, EditStationDto request, CancellationToken ct);

        /// <summary>
        /// Enables or disables a station identified by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a message about the operation result.</returns>
        Task<DisabledEnabledStationResponseDto> DisabledEnabledStation(Guid ID, CancellationToken ct);
    }
}