using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PerlaMetroApiMain.DTOs.Route;

namespace PerlaMetroApiMain.Services.Interfaces
{
    /// <summary>
    /// Defines the contract for Station service operations.  
    /// Provides methods to create, edit, retrieve, and enable/disable stations.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Creates a new station in the system.
        /// </summary>
        /// <param name="request">DTO containing the station data to be created.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the created station information.</returns>
        Task<CreateRouteResponse> CreateRoute(CreateRoute request, CancellationToken ct);

        /// <summary>
        /// Retrieves a list of stations filtered by optional parameters.
        /// </summary>
        /// <param name="Name">Optional filter for station name.</param>
        /// <param name="Type">Optional filter for station type (Origen, Destino, Intermedia).</param>
        /// <param name="State">Optional filter for station state (Activa/Inactiva).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a list of stations.</returns>
        Task<GetRoute> GetRoutes(CancellationToken ct);

        /// <summary>
        /// Retrieves a station by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the station details.</returns>
        Task<GetRoute> GetRouteById(int ID, CancellationToken ct);

        /// <summary>
        /// Edits an existing station identified by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="request">DTO containing the updated station data.</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing the updated station information.</returns>
        Task<UpdateRoute> EditRoute(int ID, updateDataRoute request, CancellationToken ct);

        /// <summary>
        /// Enables or disables a station identified by ID.
        /// </summary>
        /// <param name="ID">The ID of the station (UUID v4).</param>
        /// <param name="ct">Cancellation token to cancel the operation if needed.</param>
        /// <returns>A response DTO containing a message about the operation result.</returns>
        Task<DeleteRoute> DisabledEnabledRoute(int ID, CancellationToken ct);
    }


}