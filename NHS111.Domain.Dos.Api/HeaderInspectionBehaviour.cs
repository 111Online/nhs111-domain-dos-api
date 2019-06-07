using System.Runtime.Serialization;
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
            if (!reply.IsFault) return;
            var fault = reply.GetBody<Fault>();
            throw new FaultException(new FaultReason(fault.Reason.Text), new FaultCode(fault.Code.Value), string.Empty);
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

    [DataContract(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Fault
    {
        [DataMember]
        public Code Code;
        [DataMember]
        public Reason Reason;
    }

    [DataContract(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Code
    {
        [DataMember]
        public string Value;
    }

    [DataContract(Namespace = "http://www.w3.org/2003/05/soap-envelope")]
    public class Reason
    {
        [DataMember]
        public string Text;
    }
}
