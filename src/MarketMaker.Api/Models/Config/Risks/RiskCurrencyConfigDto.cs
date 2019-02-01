using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config.Risks
{
	public class RiskCurrencyConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("currency_id", Required = Required.AllowNull)]
		public long? CurrencyId { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("exchange")]
		public string Exchange { get; set; }

		[JsonProperty("max_long_exposure", Required = Required.AllowNull)]
		public double? MaxLongExposure { get; set; }

		[JsonProperty("max_short_exposure", Required = Required.AllowNull)]
		public double? MaxShortExposure { get; set; }

		[JsonProperty("position_max_norm_size", Required = Required.AllowNull)]
		public string PositionMaxNormSize { get; set; }

		[JsonProperty("running")]
		public bool Running { get; set; }
	}
}
