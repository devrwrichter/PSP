using AutoMapper;
using Stone.PSP.Domain.Entities;
using Stone.PSP.Domain.GLPD;
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
                    ExpirationDate = src.CardExpirationDate,
                    VerificationCode = src.CardVerificationCode
                }));

            CreateMap<TransactionViewModel, PspTransaction>()
                .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.CreditCard.Number.GetOfuscatedCreditCardNumber()))
                .ForMember(dest => dest.CardHolder, opt => opt.MapFrom(src => src.CreditCard.Holder.Trim()))
                .ForMember(dest => dest.CardExpirationDate, opt => opt.MapFrom(src => src.CreditCard.ExpirationDate))
                .ForMember(dest => dest.CardVerificationCode, opt => opt.MapFrom(src => src.CreditCard.VerificationCode.Trim()))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.Id));
        }
    }
}
