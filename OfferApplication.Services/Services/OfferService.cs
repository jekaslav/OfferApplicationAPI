using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfferApplication.Domain.Contexts;
using OfferApplication.Domain.Entities;
using OfferApplication.Domain.Models;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.Services.Services;

public class OfferService : IOfferService
{
    private OfferApplicationDbContext OfferApplicationDbContext { get; }
    
    private IMapper Mapper { get; }

    public OfferService(OfferApplicationDbContext context, IMapper mapper)
    {
        OfferApplicationDbContext = context;
        Mapper = mapper;
    }

    public async Task<IEnumerable<OfferDto>> SearchOffers(string? brand, string? model, string? provider, CancellationToken cancellationToken)
    {
        var query = OfferApplicationDbContext.Offers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand) || !string.IsNullOrWhiteSpace(model) || !string.IsNullOrWhiteSpace(provider))
        {
            if (!string.IsNullOrWhiteSpace(brand))
            {
                var lowerCaseBrand = brand.ToLower();
                query = query.Where(x => x.Brand.ToLower().Contains(lowerCaseBrand));
            }

            if (!string.IsNullOrWhiteSpace(model))
            {
                var lowerCaseModel = model.ToLower();
                query = query.Where(x => x.Model.ToLower().Contains(lowerCaseModel));
            }

            if (!string.IsNullOrWhiteSpace(provider))
            {
                var lowerCaseProvider = provider.ToLower();
                query = query.Where(x => x.Provider.Name.ToLower().Contains(lowerCaseProvider));
            }
        }
        else
        {
            throw new Exception("need 1 parameter");
        }

        var offers = await query
            .AsNoTracking()
            .Select(x => Mapper.Map<OfferDto>(x))
            .ToListAsync(cancellationToken);

        return offers;
    }

    public async Task<bool> Create(OfferDto offerDto, int providerId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(offerDto.Brand))
        {
            throw new ArgumentException();
        }

        if (string.IsNullOrWhiteSpace(offerDto.Model))
        {
            throw new ArgumentException();
        }

        var provider = await OfferApplicationDbContext.Providers.FindAsync(providerId);
        if (provider == null)
        {
            throw new ArgumentException("Invalid providerId");
        }

        var newOffer = new OfferEntity
        {
            Brand = offerDto.Brand,
            Model = offerDto.Model,
            ProviderId = providerId,
            CreatedDate = DateTimeOffset.Now
        };

        OfferApplicationDbContext.Offers.Add(newOffer);

        await OfferApplicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}