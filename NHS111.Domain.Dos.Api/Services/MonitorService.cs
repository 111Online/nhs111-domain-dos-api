using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DirectoryOfServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace NHS111.Domain.Dos.Api.Services
{
    public class MonitorService : IMonitorService
    {
        private readonly IPathwayServiceSoapFactory _pathWayServiceFactory;
        private readonly IConfiguration _configuration;

        public MonitorService(IPathwayServiceSoapFactory pathWayServiceFactory, IConfiguration configuration)
        {
            _pathWayServiceFactory = pathWayServiceFactory;
            _configuration = configuration;
        }

        private string DosUser
        {
            get { return _configuration.GetSection("dos_credential_user").Value; }
        }

        private string DosPassword
        {
            get { return _configuration.GetSection("dos_credential_password").Value; }
        }

        public string Ping()
        {
            return "pong";
        }

        public string Metrics()
        {
            return "Metrics";
        }

        public async Task<bool> Health()
        {
            try
            {
                var jsonString =
                    new StringBuilder("{\"serviceVersion\":\"1.4\",\"userInfo\":{\"username\":\"" + DosUser + "\",\"password\":\"" + DosPassword + "\"},")
                        .Append("\"c\":{\"caseRef\":\"123\",\"caseId\":\"123\",\"postcode\":\"EC1A 4JQ\",\"surgery\":\"")
                        .Append("A83046\",\"age\":35,")
                        .Append("\"ageFormat\":0,\"disposition\":1002")
                        .Append(",\"symptomGroup\":1110,\"symptomDiscriminatorList\":[4052],")
                        .Append("\"searchDistanceSpecified\":false,\"gender\":\"M\"}}").ToString();

                var checkCapacitySummaryRequest = JsonConvert.DeserializeObject<CheckCapacitySummaryRequest>(jsonString);
                var client = _pathWayServiceFactory.Create(_configuration.GetSection("dos_endpoint").Value);
                var result = await client.CheckCapacitySummaryAsync(checkCapacitySummaryRequest);

                return result != null && result.CheckCapacitySummaryResult.Any();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Version()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }

    public interface IMonitorService
    {
        string Ping();
        string Metrics();
        Task<bool> Health();
        string Version();
    }
}
