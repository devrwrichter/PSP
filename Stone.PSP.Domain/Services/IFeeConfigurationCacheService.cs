using Stone.PSP.Domain.DomainObjects;

namespace Stone.PSP.Domain.Services
{
    public interface IFeeConfigurationCacheService
    {
        FeeConfiguration GetConfiguration();
    }
}