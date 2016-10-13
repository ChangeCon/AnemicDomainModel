using System;
using System.Collections.Generic;
using System.Linq;
using Ticketing.Infrastructure.Domain;
using Ticketing.Model.Clients;
using Ticketing.Services.Contracts;
using Ticketing.Services.Mapping;
using Ticketing.Services.Messaging;

namespace Ticketing.Services
{
    public class KioskService : IKioskService
    {
        private readonly  bool _isDesperado = false;
        private readonly bool _intendedForKioskSale = true;

        IClientRepository _clientRepository;
        public KioskService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public GetClientResponse GetClient(GetClientRequest request)
        {
            GetClientResponse response = new GetClientResponse { Request = request, ResponseToken = Guid.NewGuid() };
            try
            {
                Client client = _clientRepository.FindBy(request.ClientId);

                client.GenerateCoupons(_isDesperado, _intendedForKioskSale);

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
        

    }
}
