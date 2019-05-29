using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Request
{
    public class DosCheckCapacitySummaryRequest : DosRequest
    {

        public DosCheckCapacitySummaryRequest(string userName, string password, DosCase dosCase)
        {
            UserInfo = new DosUserInfo(userName, password);
            this.Case = dosCase;
        }

        [JsonProperty("c")]
        public DosCase Case { get; private set; }
    }
}
