using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketMaker.Api.Models.Book
{
	public class L2PackageDto
	{
		[JsonProperty("timestamp")]
		public long Timestamp { get; set; }

		[JsonProperty("sequence_number")]
		public long SequenceNumber { get; set; }

		[JsonProperty("security_id")]
		public String SecurityId { get; set; }

		[JsonProperty("type")]
		[JsonConverter(typeof(EnumConverter))]
		public L2PackageType Type { get; set; }

		[JsonProperty("entries")]
		public List<L2EntryDto> Entries { get; set; }
	}
}
