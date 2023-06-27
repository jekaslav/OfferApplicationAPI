using OfferApplication.Domain.Models;

namespace OfferApplication.Services.Interfaces;

public interface IOfferService
{
    Task<IEnumerable<OfferDto>> SearchOffers(string? brand, string? model, string? provider, CancellationToken cancellationToken);
    Task<bool> Create(OfferDto offerDto, int providerId, CancellationToken cancellationToken);
}