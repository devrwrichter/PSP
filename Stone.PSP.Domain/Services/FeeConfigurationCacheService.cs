using Stone.PSP.Domain.DomainObjects;

namespace Stone.PSP.Domain.Services
{
    public class FeeConfigurationCacheService : IFeeConfigurationCacheService
    {
        private FeeConfiguration _feeConfiguration => GetFeeConfiguration();

        public FeeConfiguration GetConfiguration()
        {
            return _feeConfiguration;
        }

        /// <summary>
        /// Fake Cache para ilustrar a necessidade das regras de criação do payable
        /// </summary>
        private FeeConfiguration GetFeeConfiguration()
        {
            //Fake
            return new FeeConfiguration { Days = 30, CreditFeePercent = 0.05M, DebitFeePercent = 0.03M, FeeNumber = 1 };
        }
    }
}
