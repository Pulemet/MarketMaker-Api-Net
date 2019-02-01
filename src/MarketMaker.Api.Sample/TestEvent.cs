using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Config;
using MarketMaker.Api.Models.Statistics;

namespace MarketMaker.Api.Sample
{
    class TestEvent
    {
        public FullInstrumentConfigDto _instrument;
        public string _originalBuyMargins;
        public string _originalSellMargins;

        public void OnStatisticsMessage(AlgoInstrumentStatisticsDto[] statistics)
        {
            if (statistics == null)
                return;

            // Select trade statistic for our algorithm
            //AlgoInstrumentStatisticsDto TradeStatistic = statistics.FirstOrDefault(a => a.AlgoId == _algoId);
            //if (TradeStatistic != null)
            {
                // check condition, and change margins if needed
            }

            // Save pricer
            //mmRestService.SavePricer(_instrument.PricerConfig);
        }

        // Example: delta 1.2 = 120%
        public string ChangeMargins(string margins, double delta)
        {
            var listMargins = new List<int>(margins.Split(' ').Select(n => Convert.ToInt32(n)).ToArray());
            string newMargins = "";
            for (int i = 0; i < listMargins.Count; i++)
            {
                newMargins += (int)(listMargins[0] * delta);
                newMargins += i == listMargins.Count - 1 ? "" : " ";
            }
            return newMargins;
        }
    }
}
