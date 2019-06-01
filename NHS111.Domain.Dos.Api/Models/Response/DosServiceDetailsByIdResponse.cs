using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Models.Response
{
    public class DosServiceDetailsByIdResponse
    {
        [JsonProperty(PropertyName = "services")]
        public ServiceDetails[] Services { get; set; }
    }
}
