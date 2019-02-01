using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class RiskLimitsConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("algo_id")]
		public long AlgoId { get; set; }

		[JsonProperty("max_long_exposure")]
		public double MaxLongExposure { get; set; }

		[JsonProperty("max_short_exposure")]
		public double MaxShortExposure { get; set; }

		[JsonProperty("min_buy_quote_active_time", Required = Required.AllowNull)]
		public long? MinBuyQuoteActiveTime { get; set; }

		[JsonProperty("min_sell_quote_active_time", Required = Required.AllowNull)]
		public long? MinSellQuoteActiveTime { get; set; }
	}
}
