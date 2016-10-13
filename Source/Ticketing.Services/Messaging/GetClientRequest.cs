using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Infrastructure.Messaging;
using System.Runtime.Serialization;

namespace Ticketing.Services.Messaging
{
    [DataContract]
    public class GetClientRequest: RequestBase
    {
        [DataMember]
        public int ClientId { get; set; }
    }
}
