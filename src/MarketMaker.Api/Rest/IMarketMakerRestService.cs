using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMaker.Api.Models.Config;
using MarketMaker.Api.Models.Config.Risks;

namespace MarketMaker.Api.Rest
{
	public interface IMarketMakerRestService
	{
		void Authorize(string user, string password);
		string Token { get; }
		AlgoConfig[] GetAlgorithms();
		FullAlgoConfigDto GetAlgorithmConfiguration(string algoName);
		InstrumentConfigDto CreateInstrument(InstrumentConfigDto instrument);
		FullInstrumentConfigDto GetInstrument(long algoId);
		PricerConfigDto SavePricer(PricerConfigDto pricer);
		HedgerConfigDto SaveHedger(HedgerConfigDto hedger);
		RiskLimitsConfigDto SaveRiskLimitsConfig(RiskLimitsConfigDto riskLimitsConfigDto);
		RiskConfigDto CreateRisk(RiskConfigDto risk);
		RiskConfigDto[] GetRiskConfigurationDtos();
		RiskCurrencyConfigDto CreateCurrencyRisk(RiskCurrencyConfigDto currency);
		RiskStrategyConfigDto CreateStrategyRisk(RiskStrategyConfigDto risk);
		PricerConfigDto StartPricer(long algoId);
		HedgerConfigDto StartHedger(long algoId);
		InstrumentConfigDto StartInstrument(long algoId);
		PricerConfigDto StopPricer(long algoId);
		HedgerConfigDto StopHedger(long algoId);
		InstrumentConfigDto StopInstrument(long algoId);
		InstrumentConfigDto DeleteAlgorithm(long algoId);
		RiskCurrencyConfigDto StartCurrencyRisk(long currencyId);
		RiskCurrencyConfigDto StopCurrencyRisk(long currencyId);
		RiskCurrencyConfigDto DeleteCurrencyRisk(long currencyId);
		FullRiskConfigDto GetFullCurrencyRiskConfigurations(string algoKey);
	}
}
