using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config.Risks
{
	public class FullCurrencyConfigDto
	{
		[JsonProperty("currency_config")]
		public RiskCurrencyConfigDto RiskCurrencyConfig { get; set; }

		[JsonProperty("strategy_config")]
		public RiskStrategyConfigDto RiskStrategyConfig { get; set; }
	}
}
