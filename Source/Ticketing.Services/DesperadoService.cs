using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Domain;
using Ticketing.Model.Clients;
using Ticketing.Services.Contracts;
using Ticketing.Services.Mapping;
using Ticketing.Services.Messaging;

namespace Ticketing.Services
{
    public class DesperadoService : IDesperadoService
    {
        private readonly bool _intendedForKioskSale = false;

        IClientRepository _clientRepository;
        public DesperadoService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public GetClientResponse GetClient(GetClientRequest request)
        {
            GetClientResponse response = new GetClientResponse { Request = request, ResponseToken = Guid.NewGuid() };
            try
            {
                Client client = _clientRepository.FindBy(request.ClientId);

                client.GenerateCoupons(IsClientADesperado(request.ClientId), _intendedForKioskSale);
                
                response.Client = client.MapToViewClient();
                response.Success = true;
            }
            catch (EntityIsInvalidException invalidEx)
            {
                response.Success = false;
                response.Message = invalidEx.Message;
            }


            return response;

        }
        
        public CreateInvoiceResponse CreateInvoice(CreateInvoiceRequest request)
        {
            #region Logic for creating new invoice

            throw new NotImplementedException();







































            #endregion

            #region Send Invoice to Local Sheriff
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            #endregion
        }
        
        public bool IsClientADesperado(int clientId)
        {
            bool result;
            #region Logic to determine if a client is a Desperado

            result = true;























            #endregion

            return result;
        }
    }
}
