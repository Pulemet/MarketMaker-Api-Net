using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMaker.Api.Subscriptions
{
	public class SubscriptionFactory
	{
		private StompWebSocketService _stompWebSocketService;
		private readonly string _url;
		private readonly string _token;
		public SubscriptionFactory(string url, string token)
		{
			_url = url;
			_token = token;
			_stompWebSocketService = new StompWebSocketService(_url, _token);
		}

		public QuotesSubscription CreateQuotesSubscription()
		{
			return new QuotesSubscription(_stompWebSocketService);
		}

		public HedgerMarketDataSubscription CreateHedgerMarketDataSubscription()
		{
			return new HedgerMarketDataSubscription(_stompWebSocketService);
		}

		public RealTimeStatisticsSubscription CreateRealTimeStatisticsSubscription()
		{
			return new RealTimeStatisticsSubscription(_stompWebSocketService);
		}

		public SourceMarketDataSubscription CreateSourceMarketDataSubscription()
		{
			return new SourceMarketDataSubscription(_stompWebSocketService);
		}

		public TargetMarketDataSubscription CreateTargetMarketDataSubscription()
		{
			return new TargetMarketDataSubscription(_stompWebSocketService);
		}

		public TradingStatisticsSubscription CreateTradingStatisticsSubscription()
		{
			return new TradingStatisticsSubscription(_stompWebSocketService);
		}

	    public ExecutionsSubscription CreateExecutionsSubscription()
	    {
            return new ExecutionsSubscription(_stompWebSocketService);
	    }

	    public OrdersSubscription CreaOrdersSubscription()
	    {
            return new OrdersSubscription(_stompWebSocketService);
	    }

    }
}
