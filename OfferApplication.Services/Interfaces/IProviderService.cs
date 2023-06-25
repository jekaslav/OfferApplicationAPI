using OfferApplication.Domain.Models;

namespace OfferApplication.Services.Interfaces;

public interface IProviderService
{
    Task<IEnumerable<ProviderDto>> GetAllProviders(CancellationToken cancellationToken);
    Task<IEnumerable<ProviderDto>> GetPopularProviders(int count, CancellationToken cancellationToken);
    Task<bool> Create(ProviderDto providerDto, CancellationToken cancellationToken);
}