using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Subscriptions
{
	public class TradingStatisticsSubscription : TopicGenericSubscription<AlgoInstrumentStatisticsDto[]>
	{
		public TradingStatisticsSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService, "/topic/trading-statistics")
		{
		}
	}
}
