using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using WebSocketSharp;


namespace MarketMaker.Api.Subscriptions
{
	public class StompWebSocketService : IStompWebSocketService
	{ 
		protected const string messageHeader = "MESSAGE";
	    protected const string subIdPrefix = "subscription:";
	    protected const int subIdPrefixLength = 13;
	    protected const string msgIdPrefix = "message-id:";

	    protected readonly WebSocket _socket;
	    protected readonly string _token;
        // id=>topics
	    protected Dictionary<string, string> _topics = new Dictionary<string, string>();
        // id => handler
	    protected Dictionary<string, Action<string>> _subscribers = new Dictionary<string, Action<string>>();
	    protected Dictionary<Action<string>, HashSet<string>> _actions = new Dictionary<Action<string>, HashSet<string>>();
	    protected int subIdx = 1;
	    protected Timer _pingTimer;
	    protected const long PingIntervalMsec = 10000;

		public StompWebSocketService(string url, string token)
		{
			_socket = new WebSocket(url);
			_socket.Log.Level = LogLevel.Fatal;
			_socket.OnOpen += SocketOnOnOpen;
			_socket.OnMessage += SocketOnOnMessage;
		    //_socket.OnError += (obj, error) =>
		        //Console.WriteLine(error.Message);
            _token = token;
			_socket.Connect();
		}

	    protected virtual void SocketOnOnOpen(object sender, EventArgs eventArgs)
		{
			_pingTimer = new Timer(PingIntervalMsec);
			_pingTimer.Elapsed += _pingTimer_Elapsed;

            _socket.Send(GetConnectMessage(_token));
            //Console.WriteLine(_socket.IsAlive);
		}

		public virtual void Subscribe(string topic, Action<string> action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			string subscriptionId = subIdx.ToString();
			subIdx++;

			_socket.Send(GetSubscribeMessage(subscriptionId, topic));

		    if (_subscribers.Count == 0)
		        _pingTimer.Start();

            _subscribers.Add(subscriptionId, action);
			_topics.Add(subscriptionId, topic);
			HashSet<string> subIds;
			if (!_actions.TryGetValue(action, out subIds))
			{
				subIds = new HashSet<string>();
				_actions.Add(action, subIds);
			}
			subIds.Add(subscriptionId);
        }

	    protected virtual void SocketOnOnMessage(object sender, MessageEventArgs e)
		{
			int bodyStartIndex = e.Data.IndexOf("\n\n");
			int subIdStartIndex = e.Data.IndexOf(subIdPrefix);
			int msgIdStartIndex = e.Data.IndexOf(msgIdPrefix);
			if (bodyStartIndex > 0 && subIdStartIndex > 0 && msgIdStartIndex > 0)
			{
				int s = subIdStartIndex + subIdPrefixLength;
				string subId = e.Data.Substring(s, msgIdStartIndex - s - 1/*-1 for trailing \n*/).Replace("\\c", ":");

				//					_delayProcessor.CheckMessage(e.Data);

				string message = e.Data.Substring(bodyStartIndex);

				Action<string> action;
				if (_subscribers.TryGetValue(subId, out action))
					action(message);
			}
			else
			{
				Debug.WriteLine("Message can't be parsed, ignore.");
			}
		}

	    public virtual string GetSubscribeMessage(string subscriptionId, string topic)
	    {
            return "SUBSCRIBE\r\nid:" + subscriptionId + "\r\ndestination:" + topic + "\r\n\r\n\0";
        }

	    public virtual string GetConnectMessage(string token)
	    {
            return $"CONNECT\r\nauthorization:{token}\r\nheart-beat:{PingIntervalMsec},{PingIntervalMsec}\r\naccept-version:1.2\r\n\r\n\0";
        }

        protected void _pingTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			_pingTimer.Stop();

		    try
		    {
		        _socket.Send("\r\n");
		    }
			finally
			{
				_pingTimer.Start();
			}
		}

		public static long ConvertToUnixTimestamp(DateTime date)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan diff = date.ToUniversalTime() - origin;
			return (long)diff.TotalMilliseconds;
		}

		public bool Unsubscribe(string topic, Action<string> action)
		{
			HashSet<string> subIds = null;
			if (!_actions.TryGetValue(action, out subIds))
				return false;
			string removedSubId = null;
			foreach (var subId in subIds)
			{
				if (_topics[subId] == topic)
				{
					removedSubId = subId;
					break;
				}
			}
			if (removedSubId == null)
			{
				Debug.Assert(false);
				return false;
			}

			string unsubscribeMessage = "UNSUBSCRIBE\r\nid:" + removedSubId + "\r\n\r\n\0";
			_socket.Send(unsubscribeMessage);

			subIds.Remove(removedSubId);
			if (subIds.Count == 0)
				_actions.Remove(action);
			_topics.Remove(removedSubId);
			_subscribers.Remove(removedSubId);

            if(_subscribers.Count == 0)
                _pingTimer.Stop();

            return true;
		}
	}

	public interface IStompWebSocketService
	{
		void Subscribe(string topic, Action<string> handler);
		bool Unsubscribe(string topic, Action<string> handler);
	}
}
