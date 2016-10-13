using Ticketing.Services.Messaging;
using System.ServiceModel;

namespace Ticketing.Services.Contracts
{
    [ServiceContract(Namespace = "http://Ticketing.Services.ClientService")]
    public interface IClientService
    {
        [OperationContract]
        GetClientResponse GetClient(GetClientRequest request);
        [OperationContract]
        GetShiftReportResponse GetShiftReport(GetShiftReportRequest request);
        [OperationContract]
        CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request);
    }
}