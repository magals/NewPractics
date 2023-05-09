using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Concept.ClientServerLink.ShareLink.Hubs.BaseChannel;

public class BaseChannelHub : Hub<IBaseChannelClient_Receiver>, IBaseChannelClient_Transmiser
{
}
