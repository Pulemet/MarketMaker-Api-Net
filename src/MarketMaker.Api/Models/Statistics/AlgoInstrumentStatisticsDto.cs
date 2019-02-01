using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Statistics
{
	public class AlgoInstrumentStatisticsDto : ICloneable
	{
	    public virtual object Clone()
	    {
	        return this.MemberwiseClone();
	    }

		[JsonProperty("algo_key")]
		public String AlgoKey { get; set; }

		[JsonProperty("algo_id")]
		public long AlgoId { get; set; }

		[JsonProperty("start_time")]
		public long StartTime { get; set; }

		[JsonProperty("open_buy_qty")]
		public double OpenBuyQty { get; set; }

		[JsonProperty("open_sell_qty")]
		public double OpenSellQty { get; set; }

		[JsonProperty("trade_buy_qty")]
		public double TradeBuyQty { get; set; }

		[JsonProperty("trade_sell_qty")]
		public double TradeSellQty { get; set; }

		[JsonProperty("current_position_size")]
		public double CurrentPositionSize { get; set; }

		[JsonProperty("pnl")]
		public double Pnl { get; set; }

		[JsonProperty("unrealized_pnl")]
		public double UnrealizedPnL { get; set; }
	}
}
