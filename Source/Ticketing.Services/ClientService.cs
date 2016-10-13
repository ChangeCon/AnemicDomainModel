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
    public class ClientService : IClientService
    {
        private readonly bool _intendedForKioskSale = false;

        IClientRepository _clientRepository;
        IDesperadoService _desperadoService;

        public ClientService(IClientRepository clientRepository,
            IDesperadoService desperadoService)
        {
            _clientRepository = clientRepository;
            _desperadoService = desperadoService;
        }

        public GetClientResponse GetClient(GetClientRequest request)
        {
            GetClientResponse response = new GetClientResponse { Request = request, ResponseToken = Guid.NewGuid() };
            try
            {
                Client client = _clientRepository.FindBy(request.ClientId);

                bool isDesperado = _desperadoService.IsClientADesperado(request.ClientId);

                client.GenerateCoupons(isDesperado, _intendedForKioskSale);

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

        }

        public GetShiftReportResponse GetShiftReport(GetShiftReportRequest request)
        {
            #region Generate financial report for the end of the Shift

            throw new NotImplementedException();






































            #endregion
        }
    }
}
