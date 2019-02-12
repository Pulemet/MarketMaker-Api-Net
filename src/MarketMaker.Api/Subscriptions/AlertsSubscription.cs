using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Subscriptions
{
    public class AlertsSubscription : TopicGenericSubscription<TradingAlertDto[]>
    {
        public AlertsSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService, "/topic/alerts")
        {
        }
    }
}
