using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{
    /// <summary>
    /// DTO for editing an existing station
    /// Contains the editable fields of a station
    /// </summary>

    public class EditStationDto
    {

        /// <summary>
        /// Name of the Station
        /// </summary>
        public string NameStation { get; set; } = string.Empty;

        /// <summary>
        /// Location of the station.
        /// </summary>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Type of the station.
        /// </summary>
        /// <remarks>
        /// Required field. Valid values are: Origen, Destino, Intermedia.  
        /// If another value is provided, an error will be thrown:
        /// "El tipo debe ser Origen, Destino o Intermedia".
        /// </remarks>  
        [RegularExpression("Origen|Destino|Intermedia", ErrorMessage = "El tipo debe ser Origen, Destino o Intermedia")]
        public string Type { get; set; } = string.Empty;
    }
}