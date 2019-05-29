using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Request
{
    public class DosServiceDetailsByIdRequest : DosRequest
    {
        public DosServiceDetailsByIdRequest(string userName, string password, string serviceId)
        {
            UserInfo = new DosUserInfo(userName, password);
            this.ServiceId = serviceId;
        }

        [JsonProperty("serviceId")]
        public string ServiceId { get; private set; }
    }
}
