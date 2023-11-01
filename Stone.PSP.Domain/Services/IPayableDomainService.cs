using Stone.PSP.Domain.Entities;

namespace Stone.PSP.Domain.Services
{
    public interface IPayableDomainService
    {
        Payable GetPayable(PspTransaction transaction);
    }
}