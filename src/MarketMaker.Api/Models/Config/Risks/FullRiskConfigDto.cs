using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config.Risks
{
	public class FullRiskConfigDto
	{
		[JsonProperty("risk")]
		public RiskConfigDto Risk { get; set; }

		[JsonProperty("configurations", Required = Required.AllowNull)]
		public FullCurrencyConfigDto[] FullCurrencyConfigs { get; set; }
	}
}
