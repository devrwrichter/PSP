using AutoMapper;
using Stone.PSP.Domain.Entities;
using TransactionService.ViewModels;

namespace Stone.PSP.TransactionService.Automapper
{
    internal class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<PspTransaction, TransactionViewModel>()
                .ForMember(dest => dest.Client,opt => opt.MapFrom(src => new ClientViewModel { Id = src.ClientId }))
                .ForMember(dest => dest.CreditCard, opt => opt.MapFrom(src => new CreditCardViewModel
                {
                    Holder = src.CardHolder,
                    Number = src.CardNumber,
                    ValidateDate = src.CardExpirationDate,
                    VerificationCode = src.CardVerificationCode
                }));
        }
    }
}
