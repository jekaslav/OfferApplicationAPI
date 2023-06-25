using OfferApplication.Domain.Models;

namespace OfferApplication.Services.Interfaces;

public interface IOfferService
{
    Task<IEnumerable<OfferDto>> GetAllOffers(CancellationToken cancellationToken);
    Task<IEnumerable<OfferDto>> SearchOffers(string brand, string model, string provider, CancellationToken cancellationToken);
    Task<bool> Create(OfferDto offerDto, CancellationToken cancellationToken);
}