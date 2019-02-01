using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Statistics
{

	public class RealTimeStatisticsDto
	{
		[JsonProperty("algo_key")]
		public String AlgoKey { get; set; }

		[JsonProperty("instrument")]
		public String Instrument { get; set; }


		[JsonProperty("hist_volatility_2hrs")]
		public double HistVolatility2Hrs { get; set; }


		[JsonProperty("hist_volatility_12hrs")]
		public double HistVolatility12Hrs { get; set; }


		[JsonProperty("vpin")]
		public double Vpin { get; set; }

		[JsonProperty("liq_skew_25_bps_min")]
		public double LiqSkew25BpsMin { get; set; }

		[JsonProperty("exchange")]
		public String Exchange { get; set; }
	}
}
