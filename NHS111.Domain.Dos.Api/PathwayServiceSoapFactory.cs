using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using DirectoryOfServices;
using Microsoft.Extensions.Configuration;

namespace NHS111.Domain.Dos.Api
{
    public class PathwayServiceSoapFactory : IPathwayServiceSoapFactory
    {
        private readonly IConfiguration _configuration;
        public PathwayServiceSoapFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PathWayServiceSoap Create()
        {
            return Create(string.Empty);
        }

        public PathWayServiceSoap Create(string endpoint)
        {
            PathWayServiceSoapClient client;
            if (string.IsNullOrEmpty(endpoint) || endpoint == "Unspecified")
                client = new PathWayServiceSoapClient(PathWayServiceSoapClient.EndpointConfiguration.PathWayServiceSoap12, _configuration["DirectoryOfServices:BaseUrl"]);
            else
            {
                var endpointUrl = endpoint == "Live" ? _configuration["DirectoryOfServices:LiveBaseUrl"] : _configuration["DirectoryOfServices:UatBaseUrl"];
                client = new PathWayServiceSoapClient(PathWayServiceSoapClient.EndpointConfiguration.PathWayServiceSoap12, endpointUrl);
            }
            client.Endpoint.EndpointBehaviors.Add(new HeaderInspectionBehavior());
            return client;
        }
    }

    public interface IPathwayServiceSoapFactory
    {
        PathWayServiceSoap Create();
        PathWayServiceSoap Create(string endpoint);
    }
}
