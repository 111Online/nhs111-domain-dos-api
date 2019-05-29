using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Request
{
    public class DosRequest
    {
        [JsonProperty("ServiceVersion")]
        public string ServiceVersion { get { return "1.5"; } }

        [JsonProperty("UserInfo")]
        public DosUserInfo UserInfo { get; protected set; }
    }
}
