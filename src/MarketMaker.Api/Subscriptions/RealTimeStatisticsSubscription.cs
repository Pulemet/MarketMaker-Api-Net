using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Subscriptions
{
	public class RealTimeStatisticsSubscription : TopicGenericSubscription<RealTimeStatisticsDto[]>
	{
		public RealTimeStatisticsSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService, "/topic/real-time-statistics")
		{
		}
	}
}
