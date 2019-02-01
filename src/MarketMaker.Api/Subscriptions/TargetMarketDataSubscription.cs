using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Book;

namespace MarketMaker.Api.Subscriptions
{
	public class TargetMarketDataSubscription : EntityGenericSubscription<long, L2PackageDto>
	{
		public TargetMarketDataSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService)
		{
		}

		protected override string TopicName(long id) => "/user/topic/target-market-data/" + id;
	}
}
