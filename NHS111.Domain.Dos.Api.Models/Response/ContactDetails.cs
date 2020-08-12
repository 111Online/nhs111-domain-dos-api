using Newtonsoft.Json;
using NHS111.Domain.Dos.Api.Models.Enums;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class ContactDetails
    {
        [JsonProperty(PropertyName = "tagField")]
        public ContactType Tag { get; set; }

        [JsonProperty(PropertyName = "nameField")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "valueField")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "orderField")]
        public int Order { get; set; }
    }

}
