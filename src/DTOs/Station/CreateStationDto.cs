using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{
    public class CreateStationDto
    {

        [Required(ErrorMessage = "El nombre de la estacion es requerido")]
        public string NameStation { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ubicacion de la estacion es requerido")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de la estacion es requerido")]
        [RegularExpression("Origen|Destino|Intermedia", ErrorMessage = "El tipo debe ser Origen, Destino o Intermedia")]
        public string Type { get; set; } = string.Empty; 
    }
}