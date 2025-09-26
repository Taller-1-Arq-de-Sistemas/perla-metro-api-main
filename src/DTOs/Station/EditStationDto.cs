using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{


    public class EditStationDto
    {

        public string NameStation { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        [RegularExpression("Origen|Destino|Intermedia", ErrorMessage = "El tipo debe ser Origen, Destino o Intermedia")]
        public string Type { get; set; } = string.Empty;
    }
}