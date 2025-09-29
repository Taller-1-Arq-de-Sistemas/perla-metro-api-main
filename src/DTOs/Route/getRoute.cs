namespace PerlaMetroApiMain.DTOs.Route
{
    public class GetRoute
    {
        public string message { get; set; } = "";

        public IEnumerable<ResposeRoute> data { get; set; } = new List<ResposeRoute>();
    }


    public class ResposeRoute
    {
        public RouteDto Route { get; set; } = new();
        public required StationDto EstacionInicio { get; set; }

        public required List<StationDto> EstacionesIntermedias { get; set; } = new List<StationDto>();

        public required StationDto EstacionFinal { get; set; }


    }
    public class RouteDto
    {
        public int RouteId { get; set; }
        public string StartTime { get; set; } = "";
        public string? EndTime { get; set; }
    }
    public class StationDto
    {
        public int StationId { get; set; }

        public string name { get; set; } = "";

        public string address { get; set; } = "";

        public string stopType { get; set; } = "";
        public int stationStatus { get; set; }
    }
}