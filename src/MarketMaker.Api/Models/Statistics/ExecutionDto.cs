using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Book;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MarketMaker.Api.Models.Statistics
{
    public class ExecutionDto
    {
        internal class EnumConverter : StringEnumConverter
        {
            public EnumConverter() : base(new SnakeCaseNamingStrategy(), false)
            {
            }
        }

        [JsonProperty("order_source")]
        public String OrderSource { get; set; }

        [JsonProperty("algo_id")]
        public long AlgoId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("symbol")]
        public String Symbol { get; set; }

        [JsonProperty("side")]
        public Side Side { get; set; }

        [JsonProperty("text")]
        public String Text { get; set; }

        [JsonProperty("exchange")]
        public String Exchange { get; set; }

        [JsonProperty("execution_id")]
        public String ExecutionId { get; set; }

        [JsonProperty("order_correlation_id")]
        public long OrderCorrelationId { get; set; }

        [JsonProperty("execution_size")]
        public double ExecutionSize { get; set; }

        [JsonProperty("execution_price")]
        public double ExecutionPrice { get; set; }
    }
}
