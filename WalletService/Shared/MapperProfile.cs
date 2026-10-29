using WalletService.Features.Transactions.Dtos;
using WalletService.Features.Wallets;
using WalletService.Features.Wallets.Dtos;
using Transaction = WalletService.Features.Transactions.Transaction;

namespace WalletService.Shared;

public class MapperProfile : AutoMapper.Profile
{
    public MapperProfile()
    {
        CreateMap<UserWallet, UserWalletDto>();
        CreateMap<Transaction, TransactionDto>().ForMember(x => x.TransactionType,
            opt => opt.MapFrom(src => src.TransactionType.ToString()));
    }
}