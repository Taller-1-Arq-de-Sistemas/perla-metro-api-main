using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{
    public class GetByIdStationResponseDto
    {
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("estacion")]
        public ResponseStationByIdDto Station { get; set; } = new();
    }


    public class ResponseStationByIdDto
    {

        public Guid ID { get; set; }

        public string NameStation { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
    }
}