using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Subscriptions
{
    public class ExecutionsSubscription : EntityGenericSubscription<long, ExecutionDto[]>
    {
        public ExecutionsSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService)
        {
        }

        protected override string TopicName(long id) => "/user/topic/trades/" + id;
    }
}
