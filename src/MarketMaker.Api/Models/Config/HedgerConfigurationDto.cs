using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class HedgerConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("algo_id")]
		public long AlgoId { get; set; }

		[JsonProperty("position_max_norm_size")]
		public string PositionMaxNormSize { get; set; }

		[JsonProperty("hedge_strategy")]
		public string HedgeStrategy { get; set; }

		[JsonProperty("hedge_instrument")]
		public string HedgeInstrument { get; set; }

		[JsonProperty("execution_style")]
		public string ExecutionStyle { get; set; }

		[JsonProperty("venues_list")]
		public string VenuesList { get; set; }

		[JsonProperty("max_order_size")]
		public double MaxOrderSize { get; set; }

		[JsonProperty("resend_time")]
		public long ResendTime { get; set; }

		[JsonProperty("running")]
		public bool Running { get; set; }
    }
}
