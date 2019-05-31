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
            if (string.IsNullOrEmpty(endpoint))
                client = new PathWayServiceSoapClient(PathWayServiceSoapClient.EndpointConfiguration.PathWayServiceSoap12, _configuration["DirectoryOfServices:BaseUrl"]);
            else
            {
                var endpointUrl = endpoint == "Live" ? _configuration["dos-live-endpoint"] : _configuration["dos-uat-endpoint"];
                var uri = new Uri(endpointUrl);
                var binding = new CustomBinding();
                var httpsBindingElement = new HttpsTransportBindingElement
                {
                    MaxReceivedMessageSize = 2000000000
                };
                binding.Elements.Add(new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8));
                binding.Elements.Add(httpsBindingElement);
                var endPointAddress = new EndpointAddress(uri);
                client = new PathWayServiceSoapClient(binding, endPointAddress);
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
