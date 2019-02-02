using Newtonsoft.Json;
using System;

namespace DGT.WebApi.ViewModels
{
    public class RegisterInfractionRequest
    {
        [JsonProperty(PropertyName = "infraction_date")]
        public DateTime InfractionDate { get; set; }

        [JsonProperty(PropertyName = "driver_id")]
        public string DriverId { get; set; }
    }
}
