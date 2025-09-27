namespace perla_metro_api_main.src.DTOs.Route
{
    public class GetRoute
    {
        public string Message { get; set; } = "";

        public IEnumerable<ResposeRoute> Routes { get; set; } = new List<ResposeRoute>();
    }
    public class ResposeRoute
    {
        public int RotueId { get; set; }
        public required StationDto StationOrigin { get; set; }

        public required List<StationDto> StationIntermet { get; set; } = new List<StationDto>();

        public required StationDto StationFinal { get; set; }

        public required string StartTime { get; set; }

        public required string FinalTime { get; set; }

        

    }
    public class StationDto
    {
        public int StationId { get; set; }

        public string NameStaion { get; set; } = "";

        public string Location { get; set; } = "";

        public string Type { get; set; } = "";
    }
}