using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{
     /// <summary>
    /// DTO for the response of enabling or disabling a station.  
    /// Contains only a message indicating the result of the operation.
    /// </summary>
    public class DisabledEnabledStationResponseDto
    {
        /// <summary>
        /// Informational message about the operation result.  
        /// Example: "Station enabled successfully" or "Station disabled successfully".
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO for the response of creating or editing a station.  
    /// Contains a message and the updated station details.
    /// </summary>
    public class CreateEditStationResponseDto
    {
        /// <summary>
        /// Informational message about the operation result.  
        /// Example: "Station created successfully" or "Station updated successfully".
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Station object returned after creation or edition.  
        /// Mapped to JSON property <c>Estacion</c>.
        /// </summary>
        [JsonPropertyName("Estacion")]
        public ResponseStationDto Station { get; set; } = new();
    }

    /// <summary>
    /// DTO for the response of retrieving multiple stations.  
    /// Contains a message and a list of stations.
    /// </summary>
    public class GetStationResponseDto
    {
        /// <summary>
        /// Informational message about the operation result.  
        /// Example: "Stations retrieved successfully".
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// List of stations returned in the response.  
        /// Mapped to JSON property <c>estaciones</c>.
        /// </summary>
        [JsonPropertyName("estaciones")]
        public List<ResponseStationDto> Stations { get; set; } = new();
    }

    /// <summary>
    /// DTO representing station details used in responses.  
    /// Includes basic attributes and current state.
    /// </summary>
    public class ResponseStationDto
    {
        /// <summary>
        /// Unique identifier of the station (UUID v4).
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name of the station.
        /// </summary>
        public string NameStation { get; set; } = string.Empty;

        /// <summary>
        /// Location of station.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Type of the station.  
        /// Possible values: Origen, Destino, Intermedia.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the station is enabled true  or disabled false.
        /// </summary>
        public bool State { get; set; }
    }
}