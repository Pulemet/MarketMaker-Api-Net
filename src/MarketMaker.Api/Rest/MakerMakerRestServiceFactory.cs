using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMaker.Api.Rest
{
	public static class MakerMakerRestServiceFactory
	{
		public static IMarketMakerRestService CreateMakerRestService(string url, string oauthPath, string authorization)
		{
			var restService = new RestService(url, oauthPath, authorization);
			return new MarketMakerRestService(restService);
		}
	}
}
