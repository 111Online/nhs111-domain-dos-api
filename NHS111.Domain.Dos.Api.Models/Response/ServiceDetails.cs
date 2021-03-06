﻿using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class ServiceDetails
    {
        [JsonProperty(PropertyName = "idField")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "odsCodeField")]
        public string OdsCode { get; set; }

        [JsonProperty(PropertyName = "contactDetailsField")]
        public ContactDetails[] ContactDetails { get; set; }

        [JsonProperty(PropertyName = "serviceEndpoints")]
        public ServiceEndpoint[] ServiceEndpoints { get; set; }
    }
}
