namespace PerlaMetroApiMain.DTOs.Route
{
    public class updateDataRoute
    {
        public int final { get; set; }
        public string? horaInicio { get; set; }
        public string? horaFin { get; set; }
        public List<int>? intermedio { get; set; }
        public int estatus { get; set; }
    }
    public class UpdateRoute
    {
        public string? message { get; set; }

        public dataResponse? data { get; set; }
    }

    public class dataResponse
    {
        public int routeId { get; set; }
        public dataUpdate? datosActualizados { get; set; }
    }
    public class dataUpdate
    {
        public int routeId { get; set; }
        public int inicio { get; set; }
        public int final { get; set; }
        public List<int>? intermedio { get; set; }
        public string? startTime { get; set; }
        public string? endTime { get; set; }
        public int routeStatus { get; set; }

    }

}