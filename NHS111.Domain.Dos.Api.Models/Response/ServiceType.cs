using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class ServiceType
    {
        [JsonProperty(PropertyName = "idField")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "nameField")]
        public string Name { get; set; }
    }
}
