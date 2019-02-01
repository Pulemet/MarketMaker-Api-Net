using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class FullAlgoConfigDto
	{
		[JsonProperty("algorithm")]
		public AlgoConfig Algorithm { get; set; }

		[JsonProperty("configurations")]
		public FullInstrumentConfigDto[] FullInstrumentConfigDto { get; set; }
	}
}
