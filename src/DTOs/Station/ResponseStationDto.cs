using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace perla_metro_api_main.src.DTOs.Station
{

    public class CreateStationResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public ResponseStationDto Station { get; set; } = new();
    }

    public class GetStationResponseDto
{
    public string Message { get; set; } = string.Empty;
    public List<ResponseStationDto> Estaciones { get; set; } = new();
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