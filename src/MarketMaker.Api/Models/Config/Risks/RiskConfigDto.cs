using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config.Risks
{
	public class RiskConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("account_currency")]
		public string AccountCurrency { get; set; }

		[JsonProperty("cutoff_time")]
		public long CutoffTime { get; set; }

		[JsonProperty("last_reset_time", Required = Required.AllowNull)]
		public long? LastResetTime { get; set; }
	}
}
