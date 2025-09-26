using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{
    /// <summary>
    /// DTO for Response of operation Get By Id
    /// Contains all the station except the type information to be returned to the client
    /// </summary>
    public class GetByIdStationResponseDto
    {
        /// <summary>
        /// Informational message about the operation result.  
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Station information object returned in the response.  
        /// Mapped to JSON property "estacion".
        /// </summary>
        [JsonPropertyName("estacion")]
        public ResponseStationByIdDto Station { get; set; } = new();
    }


    /// <summary>
    /// DTO representing the details of a station returned by "Get Station by ID".
    /// </summary>
    public class ResponseStationByIdDto
    {

        /// <summary>
        /// Unique identifier UUID V4
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Name of the station
        /// </summary>
        public string NameStation { get; set; } = string.Empty;

        /// <summary>
        /// Location of the station
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Station type
        /// Possible values: Origen, Destino, Intermedia
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}