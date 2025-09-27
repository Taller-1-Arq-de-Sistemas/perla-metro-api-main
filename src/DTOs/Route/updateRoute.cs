namespace perla_metro_api_main.src.DTOs.Route
{
    public class UpdateRoute
    {
        public int Inicio { get; set; }

        public List<int>? Intermedio { get; set; }

        public int Final { get; set; }

        public string? HoraIncio { get; set; }

        public string? HoraFin { get; set; }

        public bool Estatus { get; set; } 
    }
}