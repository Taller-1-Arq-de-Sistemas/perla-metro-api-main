using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using perla_metro_api_main.src.Services.Interfaces;

namespace perla_metro_api_main.src.Services
{
    public class StationService : IStationService
    {

        private readonly string _stationUrl = null!;

        private readonly HttpClient _httpclient;

        private static readonly JsonSerializerOptions serializerOptions = new(JsonSerializerDefaults.Web);


        public StationService(IConfiguration configuration, HttpClient httpClient)
        {
            //llamar a url del servicio atraves de un env o algo
            _stationUrl = configuration.GetValue<string>("StationServiceUrl") ?? throw new InvalidOperationException("StationServiceUrl no esta configurado correctamente."); ;
            _httpclient = httpClient;
        }
        


    }
}