using Newtonsoft.Json;
using NHS111.Domain.Dos.Api.Models.Enums;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class ServiceEndpoint
    {
        [JsonProperty(PropertyName = "addressField")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "businessScenarioField")] 
        public string BusinessScenario { get; set; }
        [JsonProperty(PropertyName = "commentField")] 
        public string Comment { get; set; }

        [JsonProperty(PropertyName = "compressionField")] 
        public string Compression { get; set; }

        [JsonProperty(PropertyName = "endpointOrderField")] 
        public int EndpointOrder { get; set; }

        [JsonProperty(PropertyName = "formatField")] 
        public string Format { get; set; }

        [JsonProperty(PropertyName = "interactionField")] 
        public string Interaction { get; set; }

        [JsonProperty(PropertyName = "transportField")] 
        public ContactType Transport { get; set; }
    }
}
