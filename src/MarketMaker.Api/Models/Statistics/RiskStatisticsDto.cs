using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Statistics
{
	public class RiskStatisticsDto
	{
		[JsonProperty("algo_key")]
		public String AlgoKey { get; set; }


		[JsonProperty("currency_id")]
		public long CurrencyId { get; set; }


		[JsonProperty("open_buy_qty")]
		public double OpenBuyQty { get; set; }


		[JsonProperty("open_sell_qty")]
		public double OpenSellQty { get; set; }


		[JsonProperty("current_position_size")]
		public double CurrentPositionSize { get; set; }
	}
}
