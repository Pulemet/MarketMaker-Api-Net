using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class InstrumentConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("algo_id", Required = Required.AllowNull)]
		public long? AlgoId { get; set; }

		[JsonProperty("instrument")]
		public string Instrument { get; set; }

		[JsonProperty("exchange")]
		public string Exchange { get; set; }

		[JsonProperty("source_exchange")]
		public string SourceExchange { get; set; }

		[JsonProperty("underlyings")]
		public string Underlyings { get; set; }

		[JsonProperty("fx_leg")]
		public string FxLeg { get; set; }

		[JsonProperty("running")]
		public bool Running { get; set; }

		public override string ToString()
		{
			return "Instrument " + AlgoKey + ":" + (AlgoId ?? -1) + " " + Instrument +
				", exchange: " + Exchange +
				", source exchange: " + SourceExchange +
				", underlyings: " + (Underlyings ?? "''") +
				", fx leg: " + (FxLeg ?? "''") +
				", running : " + Running;
		}
	}
}
