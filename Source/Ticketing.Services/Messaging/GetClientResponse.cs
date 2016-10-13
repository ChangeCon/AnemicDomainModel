using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Ticketing.Infrastructure.Messaging;
using Ticketing.Services.Views;

namespace Ticketing.Services.Messaging
{
    [DataContract]
    public class GetClientResponse: ResponseBase<GetClientRequest>
    {
        [DataMember]
        public Client Client { get; set; }
    }
}
