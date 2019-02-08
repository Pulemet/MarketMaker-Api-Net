using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Subscriptions
{
    public class OrdersSubscription : EntityGenericSubscription<long, OrderDto[]>
    {
        public OrdersSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService)
        {
        }

        protected override string TopicName(long id) => "/user/topic/orders/" + id;
    }
}
