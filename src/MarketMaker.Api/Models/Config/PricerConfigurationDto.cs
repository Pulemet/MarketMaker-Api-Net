using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Config
{
	public class PricerConfigDto
	{
		[JsonProperty("algo_key")]
		public string AlgoKey { get; set; }

		[JsonProperty("algo_id")]
		public long AlgoId { get; set; }

		[JsonProperty("buy_quote_sizes")]
		public string BuyQuoteSizes { get; set; }

		[JsonProperty("sell_quote_sizes")]
		public string SellQuoteSizes { get; set; }

		[JsonProperty("buy_margins")]
		public string BuyMargins { get; set; }

		[JsonProperty("sell_margins")]
		public string SellMargins { get; set; }

		[JsonProperty("aggregation_method")]
		public string AggregationMethod { get; set; }

		[JsonProperty("min_price_change")]
		public double MinPriceChange { get; set; }

		[JsonProperty("running")]
		public bool Running { get; set; }

	    [JsonProperty("min_spread")]
	    public string MinSpread { get; set; }
    }
}
