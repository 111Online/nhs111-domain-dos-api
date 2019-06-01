using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

    public enum ContactType
    {
        dts,
        itk,
        telno,
        email,
        faxno,
    }
}
