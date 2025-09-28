namespace perla_metro_api_main.src.DTOs.Route
{
    public class CreateRouteResponse
    {
        public string? message { get; set; }
        public dataResposeCreate? data { get; set; }
    }
    public class dataResposeCreate
    {
        public int routeId { get; set; }
        public string? startTime { get; set; }
        public string? endTime { get; set; }
        public int routeStatus { get; set; }
    }
    public class CreateRoute
    {
        public int inicio { get; set; }

        public List<int>? intermedio { get; set; }

        public int final { get; set; }

        public string? horaInicio { get; set; }

        public string? horaFin { get; set; }


    }
}