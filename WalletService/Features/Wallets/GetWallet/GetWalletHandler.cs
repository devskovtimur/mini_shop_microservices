using AutoMapper;
using Shared;
using Shared.Mediator;
using WalletService.Features.Wallets.Abstractions;
using WalletService.Features.Wallets.Dtos;

namespace WalletService.Features.Wallets.GetWallet;

public class GetWalletHandler(IWalletRepository walletRepository, IMapper mapper)
    : IQueryHandler<GetWalletQuery, UserWalletDto>
{
    public async Task<Result<UserWalletDto>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
    {
        var wallet = await walletRepository.GetById(request.OwnerId, ct: cancellationToken);

        if (wallet is null)
        {
            return Result<UserWalletDto>.Failure(
                Error.NotFound($"User with {request.OwnerId} ID don't have its own wallet"));
        }

        var dto = mapper.Map<UserWalletDto>(wallet);
        return Result<UserWalletDto>.Success(dto);
    }
}