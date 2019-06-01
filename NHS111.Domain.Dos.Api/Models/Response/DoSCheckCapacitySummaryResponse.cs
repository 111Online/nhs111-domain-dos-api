using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class DosCheckCapacitySummaryResponse
    {
        public string TransactionId { get; set; }
        public string RequestedAtDateTime { get; set; }
        public string SearchDateTime { get; set; }

        [JsonProperty(PropertyName = "CheckCapacitySummaryResult")]
        public DosService[] CheckCapacitySummaryResult { get; set; }
    }
}
