using Ticketing.Services.Messaging;
using System.ServiceModel;

namespace Ticketing.Services.Contracts
{
    [ServiceContract(Namespace = "http://Ticketing.Services.DesperadoService")]
    public interface IDesperadoService
    {
        [OperationContract]
        CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request);
        [OperationContract]
        GetClientResponse GetClient(GetClientRequest request);
        [OperationContract]
        bool IsClientADesperado(int clientId);
    }
}