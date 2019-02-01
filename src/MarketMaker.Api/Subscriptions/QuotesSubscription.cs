﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Book;
using Newtonsoft.Json;

namespace MarketMaker.Api.Subscriptions
{
	public class QuotesSubscription : EntityGenericSubscription<long, L2PackageDto>
	{
		public QuotesSubscription(IStompWebSocketService stompWebSocketService) : base(stompWebSocketService)
		{
		}

		protected override string TopicName(long id) => "/topic/quotes/" + id;
	}
}
