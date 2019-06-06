using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace NHS111.Domain.Dos.Api
{
    public class HeaderInspectionBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new HeaderInspector());
        }

        public void Validate(ServiceEndpoint endpoint) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }
    }

    public class HeaderInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState){
            if (reply.IsFault)
            {
                //https://stackoverflow.com/questions/27235601/capturing-soap-faults-and-handling-exceptions
                var fault = new FaultException();
                throw new FaultException();
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            request.Headers.RemoveAll("serviceVersion", "https://nww.pathwaysdos.nhs.uk/app/api/webservices");
            var header = new MessageHeader<string>("1.5");
            var untyped = header.GetUntypedHeader("serviceVersion", string.Empty);
            request.Headers.Add(untyped);
            return null;
        }
    }
}
