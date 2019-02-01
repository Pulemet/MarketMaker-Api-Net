using System;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config.Risks
{
	public class RiskStrategyConfigDto
	{
		[JsonProperty("algo_key")]
		public String AlgoKey { get; set; }

		[JsonProperty("currency_id")]
		public long CurrencyId { get; set; }

		[JsonProperty("hedge_strategy")]
		public String HedgeStrategy { get; set; }

		[JsonProperty("time_in_force")]
		public String TimeInForce { get; set; }

		[JsonProperty("venues_list")]
		public String VenuesList { get; set; }

		[JsonProperty("max_order_size")]
		public double MaxOrderSize { get; set; }

		[JsonProperty("resend_time")]
		public long ResendTime { get; set; }
	}
}
