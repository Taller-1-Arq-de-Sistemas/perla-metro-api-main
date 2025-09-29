namespace PerlaMetroApiMain.DTOs.Station
{
    public class responseUpdateDto
    {
        public string? message { get; set; }
        public responseData? data { get; set; }
        public string? error { get; set; }
    }
    public class resposeCreateDto
    {
        public string? message { get; set; }
        public IEnumerable<responseData>? data { get; set; }
        public string? error { get; set; }

    }
    public class responseData
    {
        public Guid ID { get; set; }
        public string? NameStation { get; set; }
        public string? Location { get; set; }
        public string? Type { get; set; }
        public bool State { get; set; }
    }
}
