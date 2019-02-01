using MarketMaker.Api.Models.Config;
using MarketMaker.Api.Models.Config.Risks;

namespace MarketMaker.Api.Rest
{
	internal class MarketMakerRestService : IMarketMakerRestService
	{
		private readonly IRestService _restService;
		internal MarketMakerRestService(IRestService restService)
		{
			_restService = restService;
		}

		public void Authorize(string user, string password)
		{
			_restService.Authorize(user, password);
		}

		public string Token => _restService.Token;

		public AlgoConfig[] GetAlgorithms()
		{
			return _restService.Get<AlgoConfig[]>("/api/v0/config/algorithms");
		}

		public FullInstrumentConfigDto GetInstrument(long algoId)
		{
			return _restService.Get<FullInstrumentConfigDto>("/api/v0/config/instrument/get/" + algoId);
		}

		public FullAlgoConfigDto GetAlgorithmConfiguration(string algoName)
		{
			return _restService.Get<FullAlgoConfigDto>("/api/v0/config/algorithm/" + algoName);
		}

		public InstrumentConfigDto CreateInstrument(InstrumentConfigDto instrument)
		{
			return _restService.Post<InstrumentConfigDto, InstrumentConfigDto>("/api/v0/config/instrument/save", instrument);
		}

		public RiskConfigDto CreateRisk(RiskConfigDto risk)
		{
			return _restService.Post<RiskConfigDto, RiskConfigDto>("/api/v0/config/risks/save", risk);
		}

		public RiskCurrencyConfigDto CreateCurrencyRisk(RiskCurrencyConfigDto currency)
		{
			return _restService.Post<RiskCurrencyConfigDto,RiskCurrencyConfigDto>(
				"/api/v0/config/risks/currency/save", currency);
		}

		public RiskStrategyConfigDto CreateStrategyRisk(RiskStrategyConfigDto risk)
		{
			return _restService.Post<RiskStrategyConfigDto, RiskStrategyConfigDto>(
				"/api/v0/config/risks/strategy/save", risk);
		}

		public PricerConfigDto StartPricer(long algoId)
		{
			return _restService.Post<PricerConfigDto>("/api/v0/config/pricer/start/" + algoId);
		}

		public HedgerConfigDto StartHedger(long algoId)
		{
			return _restService.Post<HedgerConfigDto>("/api/v0/config/hedger/start/" + algoId);
		}

		public InstrumentConfigDto StartInstrument(long algoId)
		{
			return _restService.Post<InstrumentConfigDto>("/api/v0/config/instrument/start/" + algoId);
		}

		public PricerConfigDto StopPricer(long algoId)
		{
			return _restService.Post<PricerConfigDto>("/api/v0/config/pricer/stop/" + algoId);
		}

		public HedgerConfigDto StopHedger(long algoId)
		{
			return _restService.Post<HedgerConfigDto>("/api/v0/config/hedger/stop/" + algoId);
		}

		public InstrumentConfigDto StopInstrument(long algoId)
		{
			return _restService.Post<InstrumentConfigDto>("/api/v0/config/instrument/stop/" + algoId);
		}

		public InstrumentConfigDto DeleteAlgorithm(long algoId)
		{
			return _restService.Delete<InstrumentConfigDto>("/api/v0/config/instrument/delete/" + algoId);
		}

		public RiskCurrencyConfigDto StartCurrencyRisk(long currencyId)
		{
			return _restService.Post<RiskCurrencyConfigDto>("/api/v0/config/risks/currency/start/" + currencyId);
		}

		public RiskCurrencyConfigDto StopCurrencyRisk(long currencyId)
		{
			return _restService.Post<RiskCurrencyConfigDto>("/api/v0/config/risks/currency/stop/" + currencyId);
		}

		public RiskCurrencyConfigDto DeleteCurrencyRisk(long currencyId)
		{
			return _restService.Delete<RiskCurrencyConfigDto>("/api/v0/config/risks/currency/delete/" + currencyId);
		}

		public PricerConfigDto SavePricer(PricerConfigDto pricer)
		{
			return _restService.Post<PricerConfigDto, PricerConfigDto>("/api/v0/config/pricer/save", pricer);
		}

		public HedgerConfigDto SaveHedger(HedgerConfigDto hedger)
		{
			return _restService.Post<HedgerConfigDto, HedgerConfigDto>(
				"/api/v0/config/hedger/save", hedger);
		}

		public RiskLimitsConfigDto SaveRiskLimitsConfig(RiskLimitsConfigDto riskLimitsConfigDto)
		{
			return _restService.Post<RiskLimitsConfigDto, RiskLimitsConfigDto>(
				"/api/v0/config/riskLimits/save", riskLimitsConfigDto);
		}

		public FullRiskConfigDto GetFullCurrencyRiskConfigurations(string algoKey)
		{
			return _restService.Get<FullRiskConfigDto>("/api/v0/config/risks/" + algoKey);
		}

		public RiskConfigDto[] GetRiskConfigurationDtos()
		{
			return _restService.Get<RiskConfigDto[]>("/api/v0/config/risks");
		}
	}
}
