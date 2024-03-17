using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CouchifyTravelApi.Models
{ 
    public class Airline
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iata")]
        public string Iata { get; set; }

        [JsonPropertyName("icao")]
        public string Icao { get; set; }

        [JsonPropertyName("callsign")]
        public string CallSign { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
