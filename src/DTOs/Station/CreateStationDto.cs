using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerlaMetroApiMain.DTOs.Station
{
    /// <summary>
    /// DTO for creating a new station
    /// Contains the data needed to create a station in the system
    /// </summary>
    public class CreateStationDto
    {

        /// <summary>
        /// Name of the station.
        /// </summary>
        /// <remarks>
        /// Required field. If not provided, an error will be thrown:
        /// "El nombre de la estacion es requerido".
        /// </remarks>
        [Required(ErrorMessage = "El nombre de la estacion es requerido")]
        public string NameStation { get; set; } = string.Empty;

        /// <summary>
        /// location of the station.
        /// </summary>
        /// <remarks>
        /// Required field. If not provided, an error will be thrown:
        /// "La ubicacion de la estacion es requerido".
        /// </remarks>
        [Required(ErrorMessage = "La ubicacion de la estacion es requerido")]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Type of the station.
        /// </summary>
        /// <remarks>
        /// Required field. Valid values are: Origen, Destino, Intermedia.  
        /// If another value is provided, an error will be thrown:
        /// "El tipo debe ser Origen, Destino o Intermedia".
        /// </remarks>
        [Required(ErrorMessage = "El tipo de la estacion es requerido")]
        [RegularExpression("Origen|Destino|Intermedia", ErrorMessage = "El tipo debe ser Origen, Destino o Intermedia")]
        public string Type { get; set; } = string.Empty;
    }
}