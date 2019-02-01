using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Subscriptions
{
	public class TopicGenericSubscription<T>
	{
		private readonly IStompWebSocketService _stompWebSocketService;
		private readonly string _topic;
		Dictionary<Action<T>, Action<string>> handlers = new Dictionary<Action<T>, Action<string>>();

		internal TopicGenericSubscription(IStompWebSocketService stompWebSocketService, string topic)
		{
			_stompWebSocketService = stompWebSocketService;
			_topic = topic;
		}

		public void Subscribe(Action<T> handler)
		{
			Action<string> h = s => handler(JsonConvert.DeserializeObject<T>(s));
			handlers.Add(handler, h);
			_stompWebSocketService.Subscribe(_topic, h);
		}

		public void Unsubscribe(Action<T> handler)
		{
			Action<string> h;
			if (handlers.TryGetValue(handler, out h) &&
				_stompWebSocketService.Unsubscribe(_topic, h))
			{
				handlers.Remove(handler);
			}
		}
	}
}
