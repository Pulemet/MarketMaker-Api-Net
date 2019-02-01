using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Subscriptions
{
	public abstract class EntityGenericSubscription<K, T>
	{
		private readonly IStompWebSocketService _stompWebSocketService;
		internal EntityGenericSubscription(IStompWebSocketService stompWebSocketService)
		{
			_stompWebSocketService = stompWebSocketService;
		}

		Dictionary<Action<T>, Action<string>> handlers = new Dictionary<Action<T>, Action<string>>();

		public void Subscribe(K id, Action<T> handler)
		{
			Action<string> h = s => handler(JsonConvert.DeserializeObject<T>(s));
			handlers.Add(handler, h);
			_stompWebSocketService.Subscribe(TopicName(id), h);
		}

		public void Unsubscribe(K id, Action<T> handler)
		{
			Action<string> h;
			if (handlers.TryGetValue(handler, out h) &&
				_stompWebSocketService.Unsubscribe(TopicName(id), h))
			{
				handlers.Remove(handler);
			}
		}

		protected abstract string TopicName(K id);
	}
}
