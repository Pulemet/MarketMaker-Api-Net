using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class FullInstrumentConfigDto
	{
		[JsonProperty("instrument_config")]
		public InstrumentConfigDto InstrumentConfig { get; set; }

		[JsonProperty("pricer_config")]
		public PricerConfigDto PricerConfig { get; set; }

		[JsonProperty("hedger_config")]
		public HedgerConfigDto HedgerConfig { get; set; }

		[JsonProperty("risk_limits_config")]
		public RiskLimitsConfigDto RiskLimitsConfig { get; set; }
	}
}
