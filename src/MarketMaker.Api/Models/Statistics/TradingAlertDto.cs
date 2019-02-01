using System;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Statistics
{
	public class TradingAlertDto
	{
		[JsonProperty("symbol")]
		public string Symbol { get; set; }

		[JsonProperty("algo_id")]
		public long AlgoId { get; set; }

		[JsonProperty("level")]
		public AlertLevel Level { get; set; }

		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("sequence")]
		public long Sequence { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("action")]
		public string Action { get; set; }

		[JsonProperty("duration")]
		public long Duration { get; set; }

		[JsonProperty("correlation_order_id")]
		public string CorrelationOrderId { get; set; }

		[JsonProperty("exchange")]
		public string Exchange { get; set; }

		[JsonIgnore]
		public bool modified = false;
	}

	public enum AlertLevel
	{
		Info,
		Warning,
		Error
	}
}
