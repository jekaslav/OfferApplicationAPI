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

    public async Task<IEnumerable<OfferDto>> GetAllOffers(CancellationToken cancellationToken)
    {
        var offers = await OfferApplicationDbContext.Offers
            .AsNoTracking()
            .Select(x => Mapper.Map<OfferDto>(x))
            .ToListAsync(cancellationToken);

        if (offers is null)
        {
            throw new NullReferenceException();
        }

        return offers;
    }

    public async Task<IEnumerable<OfferDto>> SearchOffers(string brand, string model, string provider,
        CancellationToken cancellationToken)
    {
        var query = OfferApplicationDbContext.Offers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(x => x.Brand == brand);
        }

        if (!string.IsNullOrWhiteSpace(model))
        {
            query = query.Where(x => x.Model == model);
        }

        if (!string.IsNullOrWhiteSpace(provider))
        {
            query = query.Where(x => x.Provider.Name == provider);
        }

        var offers = await query
            .AsNoTracking()
            .Select(x => Mapper.Map<OfferDto>(x))
            .ToListAsync(cancellationToken);

        if (offers is null)
        {
            throw new NullReferenceException();
        }

        return offers;
    }

    public async Task<bool> Create(OfferDto offerDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(offerDto.Brand))
        {
            throw new ArgumentException();
        }

        if (string.IsNullOrWhiteSpace(offerDto.Model))
        {
            throw new ArgumentException();
        }

        var newOffer = new OfferEntity
        {
            Id = offerDto.Id,
            Brand = offerDto.Brand,
            Model = offerDto.Model,
            CreatedDate = DateTimeOffset.Now
        };

        OfferApplicationDbContext.Offers.Add(newOffer);

        await OfferApplicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}