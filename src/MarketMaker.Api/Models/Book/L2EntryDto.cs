﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace MarketMaker.Api.Models.Book
{
	public enum L2Action
	{
		UPDATE,
		INSERT,
		DELETE,
		DELETE_FROM,
		DELETE_THROUGH,
		TRADE
	}
	public enum Side
	{
		BUY,
		SELL
	}
	public enum L2PackageType
	{
		SNAPSHOT_FULL_REFRESH,
		INCREMENTAL_UPDATE
	}

	internal class EnumConverter : StringEnumConverter
	{
		public EnumConverter() : base(new SnakeCaseNamingStrategy(), false)
		{
		}
	}

	public class L2EntryDto
	{
		[JsonProperty("side")]
		[JsonConverter(typeof(EnumConverter))]
		public Side Side { get; set; }

		[JsonProperty("level")]
		public short Level { get; set; }

		[JsonProperty("action")]
		[JsonConverter(typeof(EnumConverter))]
		public L2Action Action { get; set; }

		[JsonProperty("exchange_id")]
		public String ExchangeId { get; set; }

		[JsonProperty("price")]
		public double Price { get; set; }

		[JsonProperty("quantity")]
		public double Quantity { get; set; }

		[JsonProperty("number_of_orders")]
		public long NumberOfOrders { get; set; }
	}
}
