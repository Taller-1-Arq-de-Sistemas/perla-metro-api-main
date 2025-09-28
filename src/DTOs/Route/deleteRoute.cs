namespace PerlaMetroApiMain.DTOs.Route
{
    public class DeleteRoute
    {

        public string message { get; set; } = "";

        public string? error { get; set; }
        public required DataResponse data { get; set; }


    }
    public class DataResponse
    {
        public int routeId { get; set; }

        public string? StartTime { get; set; }
        public string? endTime { get; set; }
        public int routeStatus { get; set; }
    }
}