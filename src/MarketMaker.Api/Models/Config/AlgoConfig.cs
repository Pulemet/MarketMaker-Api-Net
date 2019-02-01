using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class AlgoConfig
	{
		[JsonProperty("key")]
		public string Key { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }
	}
}
