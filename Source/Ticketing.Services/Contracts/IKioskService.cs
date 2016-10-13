using Ticketing.Services.Messaging;
using System.ServiceModel;
namespace Ticketing.Services.Contracts
{
    [ServiceContract(Namespace = "http://Ticketing.Services.KioskService")]
    public interface IKioskService
    {
        [OperationContract]
        GetClientResponse GetClient(GetClientRequest request);
    }
}