using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{

    public class CreateEditStationResponseDto
    {
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("Estacion")]
        public ResponseStationDto Station { get; set; } = new();
    }

    public class GetStationResponseDto
{
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("estaciones")]
    public List<ResponseStationDto> Stations { get; set; } = new();
}

    public class ResponseStationDto
    {

        public Guid ID { get; set; }

        public string NameStation { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool State { get; set; }
    }
}