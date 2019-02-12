using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MarketMaker.Api.Models.Book;
using MarketMaker.Api.Models.Config;
using MarketMaker.Api.Models.Config.Risks;
using MarketMaker.Api.Models.Statistics;
using MarketMaker.Api.Rest;
using MarketMaker.Api.Subscriptions;
using Newtonsoft.Json;

namespace MMApi
{
	class Program
	{
		private static string baseUrl = "";
		private const string authorization = "Basic bW13ZWJ1aTptbQ==";
		private static IMarketMakerRestService mmRestService;

	    private static int _algoId = 240;
	    private static FullInstrumentConfigDto _instrument;
	    private static string _originalBuyMargins;
	    private static string _originalSellMargins;
	    private static bool _isChange = false;

        // Thresholds of changing current position size for changing margins. 
	    private static double _topMarginThreshold = 0.55;
	    private static double _bottomMarginThreshold = 0.45;


        public static void OnStatisticsMessage(AlgoInstrumentStatisticsDto[] statistics)
	    {
            if(statistics == null)
                return;

	        bool isSavePricer = _isChange;
            // Select trade statistic for our algorithm
            var tradeStatistic = statistics.FirstOrDefault(a => a.AlgoId == _algoId);
	        if (tradeStatistic != null)
	        {
                //Console.WriteLine("Current position size: {0}", tradeStatistic.CurrentPositionSize);
	            if (tradeStatistic.CurrentPositionSize < 0)
	            {
	                if (Math.Abs(tradeStatistic.CurrentPositionSize) >
	                    _instrument.RiskLimitsConfig.MaxShortExposure * _topMarginThreshold && !_isChange)
	                {
	                    _instrument.PricerConfig.SellMargins = ChangeMargins(_originalSellMargins, 1.5);
	                    _isChange = true;
	                }
                    if (Math.Abs(tradeStatistic.CurrentPositionSize) <
                            _instrument.RiskLimitsConfig.MaxShortExposure * _bottomMarginThreshold && _isChange)
	                {
	                    _instrument.PricerConfig.SellMargins = ChangeMargins(_originalSellMargins, 1.0);
	                    _isChange = false;
	                }
	            }
	            if (tradeStatistic.CurrentPositionSize > 0)
	            {
	                if (Math.Abs(tradeStatistic.CurrentPositionSize) >
	                    _instrument.RiskLimitsConfig.MaxLongExposure * _topMarginThreshold && !_isChange)
	                {
	                    _instrument.PricerConfig.BuyMargins = ChangeMargins(_originalBuyMargins, 1.5);
	                    _isChange = true;
	                }
	                if (Math.Abs(tradeStatistic.CurrentPositionSize) <
	                    _instrument.RiskLimitsConfig.MaxLongExposure * _bottomMarginThreshold && _isChange)
	                {
	                    _instrument.PricerConfig.BuyMargins = ChangeMargins(_originalBuyMargins, 1.0);
	                    _isChange = false;
	                }
	            }
                // check condition, and change margins if needed
            }
            // Save pricer
            if(isSavePricer != _isChange)
               mmRestService.SavePricer(_instrument.PricerConfig);
	    }

        // Example: delta 1.2 = 120%
	    public static string ChangeMargins(string margins, double delta)
	    {
	        var listMargins = new List<int>(margins.Split(' ').Select(n => Convert.ToInt32(n)).ToArray());
	        string newMargins = "";
	        for (int i = 0; i < listMargins.Count; i++)
	        {
	            newMargins += (int)(listMargins[i] * delta);
	            newMargins += i == listMargins.Count - 1 ? "" : " ";
	        }
	        return newMargins;
	    }

        static void Main(string[] args)
		{
            try
			{
				baseUrl = args.Length > 0 ? args[0] : null;
				if (string.IsNullOrEmpty(baseUrl))
				{
					Console.WriteLine("String is null or empty");
					return;
				}
				mmRestService = MarketMakerRestServiceFactory.CreateMakerRestService(baseUrl, "/oauth/token", authorization);
				mmRestService.Authorize("admin", "admin");


				var ws = new SubscriptionFactory("wss://18.218.146.41:8990/websocket/v0", mmRestService.Token);

                // Receive instrument by ID
			    _instrument = mmRestService.GetInstrument(_algoId);
			    _originalBuyMargins = _instrument.PricerConfig.BuyMargins;
			    _originalSellMargins = _instrument.PricerConfig.SellMargins;

                var tradeStatisticsSubs = ws.CreateTradingStatisticsSubscription();

                // Subscribe for trade statistics
			    tradeStatisticsSubs.Subscribe(OnStatisticsMessage);
			    
			    Console.ReadLine();
                tradeStatisticsSubs.Unsubscribe(OnStatisticsMessage);

                /*
                Console.Clear();
                var menuItems = GetMenuItems();
                var options = GetMenuOptions();
                ShowOptions(menuItems, options);
                while (true)
                {
                    Console.Write("Select option and press Enter:");
                    var s = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(s))
                        s = " ";
                    else
                        s = s.ToLower();
                    if (s.Contains("clear") || s.Contains("cls"))
                    {
                        Console.Clear();
                        ShowOptions(menuItems, options);
                        continue;
                    }
                    if (s.Contains("exit") || s.Contains("quit"))
                    {
                        return;
                    }
                    var option = GetChoice(options, s[0]);
                    if (option == -1 || option >= menuItems.Length)
                    {
                        Console.WriteLine("Incorrect choice.");
                        Console.WriteLine();
                        ShowOptions(menuItems, options);
                        continue;
                    }
                    try
                    {
                        Console.WriteLine($"('{menuItems[option].Item1}' was selected)");
                        menuItems[option].Item2();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                */
            }
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		private static void ShowOptions(Tuple<string, Action>[] menuItems, char[] options)
		{
			for (int i = 0; i < menuItems.Length; i++)
			{
				Console.WriteLine(options[i] + " - " + menuItems[i].Item1);
			}
		}


		public static char[] GetMenuOptions()
		{
			List<char> options = new List<char>();
			for (char i = '0'; i <= '9'; i++) options.Add(i);
			for (char i = 'a'; i <= 'y'; i++) options.Add(i);
			return options.ToArray();
		}

		public static int GetChoice(char[] options, char c)
		{
			for (int i = 0; i < options.Length; i++)
			{
				if (c == options[i])
					return i;
			}
			return -1;
		}

		public static Tuple<string, Action>[] GetMenuItems()
		{
			return new[]
			{
				new Tuple<string, Action>("Algorithms: List of supported algorithms", ShowListOfSupportedAlgorithms),
				new Tuple<string, Action>("Algorithms: Full algorithm configuration", ShowFullAlgorithmConfiguration),
				new Tuple<string, Action>("Algorithms: Create/Update instrument", DoUpdateInstrument),
				new Tuple<string, Action>("Algorithms: Get instrument", ShowInstrument),
				new Tuple<string, Action>("Algorithms: Delete instrument", DoDeleteInstrument),
				new Tuple<string, Action>("Algorithms: Start trading", DoStartTrading),
				new Tuple<string, Action>("Algorithms: Stop trading", DoStopTrading),
				new Tuple<string, Action>("Pricer configuration: Create/Update pricer", DoCreateUpdatePricer),
				new Tuple<string, Action>("Pricer configuration: Start pricer", DoStartPricer),
				new Tuple<string, Action>("Pricer configuration: Stop pricer", DoStopPricer),

				new Tuple<string, Action>("Hedger configuration: Create/Update hedger", DoCreateUpdateHedger),
				new Tuple<string, Action>("Hedger configuration: Start hedger", DoStartHedger),
				new Tuple<string, Action>("Hedger configuration: Stop hedger", DoStopHedger),

				new Tuple<string, Action>("Risk limits configuration:  Create/Update risk limits", CreateUpdateRiskLimits),
				new Tuple<string, Action>("Risk configuration: Get risk configurations", GetRiskConfiguration),
				new Tuple<string, Action>("Risk configuration: Create/Update risk configuration", CreateUpdateRiskConfiguration),
				new Tuple<string, Action>("Risk configuration: Full risk configuration", GetFullRiskConfiguration),
				new Tuple<string, Action>("Risk currency configuration: Create/Update risk currency configuration", CreateUpdateRiskCurrencyConfiguration),
				new Tuple<string, Action>("Risk currency configuration: Delete risk currency configuration", DeleteRiskCurrencyConfiguration),
				new Tuple<string, Action>("Risk currency configuration: Start risk currency", StartRiskCurrency),
				new Tuple<string, Action>("Risk currency configuration: Stop risk currency", StopRiskCurrency),

				new Tuple<string, Action>("Risk strategy configuration: Create/Update risk strategy configuration", CreateUpdateRiskStrategyConfiguration),
			};
		}

		private static void GetFullRiskConfiguration()
		{
			ShowIndentedJson(mmRestService.GetFullCurrencyRiskConfigurations("MM"));
		}

		private static void CreateUpdateRiskStrategyConfiguration()
		{
			var p = JsonConvert.DeserializeObject<RiskStrategyConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.CreateStrategyRisk(p));
		}

		private static void StopRiskCurrency()
		{
			Console.Write("Enter currency id and Press Enter:");
			long? currId = TryReadLong();
			if (currId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StopCurrencyRisk(currId.Value));
		}

		private static void StartRiskCurrency()
		{
			Console.Write("Enter currency id and Press Enter:");
			long? currId = TryReadLong();
			if (currId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StartCurrencyRisk(currId.Value));
		}

		private static void DeleteRiskCurrencyConfiguration()
		{
			Console.Write("Enter currency id and Press Enter:");
			long? currId = TryReadLong();
			if (currId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			mmRestService.DeleteCurrencyRisk(currId.Value);
		}

		private static void CreateUpdateRiskCurrencyConfiguration()
		{
			var p = JsonConvert.DeserializeObject<RiskCurrencyConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.CreateCurrencyRisk(p));
		}

		private static void CreateUpdateRiskConfiguration()
		{
			var p = JsonConvert.DeserializeObject<RiskConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.CreateRisk(p));
		}

		private static void GetRiskConfiguration()
		{
			ShowIndentedJson(mmRestService.GetRiskConfigurationDtos());
		}

		private static void CreateUpdateRiskLimits()
		{
			var p = JsonConvert.DeserializeObject<RiskLimitsConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.SaveRiskLimitsConfig(p));
		}

		private static void DoStartHedger()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StartHedger(algoId.Value));
		}

		private static void DoStopHedger()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StopHedger(algoId.Value));
		}

		private static void DoCreateUpdateHedger()
		{
			HedgerConfigDto p = JsonConvert.DeserializeObject<HedgerConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.SaveHedger(p));
		}

		private static void DoStopPricer()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StopPricer(algoId.Value));
		}

		private static void DoStartPricer()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.StartPricer(algoId.Value));
		}

		private static void DoCreateUpdatePricer()
		{
			PricerConfigDto p = JsonConvert.DeserializeObject<PricerConfigDto>(ReadMultiLine());
			ShowIndentedJson(mmRestService.SavePricer(p));
		}

		private static void ShowListOfSupportedAlgorithms()
		{
			ShowIndentedJson(mmRestService.GetAlgorithms());
		}

		private static void ShowFullAlgorithmConfiguration()
		{
			ShowIndentedJson(mmRestService.GetAlgorithmConfiguration("MM"));
		}

		private static void DoUpdateInstrument()
		{
			InstrumentConfigDto instrument = mmRestService.CreateInstrument(JsonConvert.DeserializeObject<InstrumentConfigDto>(ReadMultiLine()));
			ShowIndentedJson(instrument);
		}

		static string ReadMultiLine()
		{
			string s = Console.In.ReadToEnd();
			s = s.Trim(' ', '\n', '\r', (char)26);
			return s;
		}

		private static void ShowIndentedJson<T>(T o)
		{
			var s = JsonConvert.SerializeObject(o, Formatting.Indented);
			Console.WriteLine(s);
		}

		private static void ShowInstrument()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			ShowIndentedJson(mmRestService.GetInstrument(algoId.Value));
		}

		private static void DoDeleteInstrument()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			// delete algorithm
			ShowIndentedJson(mmRestService.DeleteAlgorithm(algoId.Value));
		}

		private static void DoStartTrading()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			// delete algorithm
			ShowIndentedJson(mmRestService.StartInstrument(algoId.Value));
		}

		private static void DoStopTrading()
		{
			Console.Write("Enter algorithm id and Press Enter:");
			long? algoId = TryReadLong();
			if (algoId == null)
			{
				Console.WriteLine("Invalid value.");
				return;
			}
			// delete algorithm
			ShowIndentedJson(mmRestService.StopInstrument(algoId.Value));
		}

		static long? TryReadLong()
		{
			long id;
			return long.TryParse(Console.ReadLine(), out id) ? (long?)id : null;
		}
	}
}
