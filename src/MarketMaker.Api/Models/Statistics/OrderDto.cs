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
    public enum Initiator
    {
        QUOTER,
        HEDGER
    }

    public enum OrderStatus
    {
        PENDING_NEW,
        NEW,
        REJECTED,
        PENDING_CANCEL,
        CANCELED,
        PENDING_REPLACE,
        REPLACED,
        PARTIALLY_FILLED,
        COMPLETELY_FILLED,
        EXPIRED,
        SUSPENDED
    }

    public class OrderDto
    {
        private const double Delta = 1e-10;
        internal class EnumConverter : StringEnumConverter
        {
            public EnumConverter() : base(new SnakeCaseNamingStrategy(), false)
            {
            }
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("source")]
        public String Source { get; set; }

        [JsonProperty("symbol")]
        public String Symbol { get; set; }

        [JsonProperty("side")]
        [JsonConverter(typeof(EnumConverter))]
        public Side Side { get; set; }

        [JsonProperty("exchange")]
        public String Exchange { get; set; }

        [JsonProperty("text")]
        public String Text { get; set; }

        [JsonProperty("initiator")]
        [JsonConverter(typeof(Book.EnumConverter))]
        public Initiator Initiator { get; set; }

        [JsonProperty("size")]
        public double Size { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("correlation_id")]
        public long CorrelationId { get; set; }

        [JsonProperty("order_type")]
        public String OrderType { get; set; }

        [JsonProperty("time_in_force")]
        public String TimeInForce { get; set; }

        [JsonProperty("order_status")]
        [JsonConverter(typeof(Book.EnumConverter))]
        public OrderStatus OrderStatus { get; set; }

        [JsonProperty("algo_id")]
        public long AlgoId { get; set; }

        [JsonProperty("executed_size")]
        public double ExecutedSize { get; set; }

        public bool EqualsExceptPrice(OrderDto order)
        {
            return order.AlgoId == AlgoId &&
                   order.CorrelationId == CorrelationId &&
                   order.Exchange == Exchange &&
                   order.OrderStatus == OrderStatus &&
                   Math.Abs(order.ExecutedSize - ExecutedSize) < Delta &&
                   order.Initiator == Initiator &&
                   order.Side == Side &&
                   Math.Abs(order.Size - Size) < Delta &&
                   order.Source == Source &&
                   order.Symbol == Symbol &&
                   order.TimeInForce == TimeInForce;
        }
    }
}
