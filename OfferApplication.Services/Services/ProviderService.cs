using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OfferApplication.Domain.Contexts;
using OfferApplication.Domain.Entities;
using OfferApplication.Domain.Models;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.Services.Services;

public class ProviderService : IProviderService
{
    private OfferApplicationDbContext OfferApplicationDbContext { get; }
    
    private IMapper Mapper { get; }

    public ProviderService(OfferApplicationDbContext context, IMapper mapper)
    {
        OfferApplicationDbContext = context;
        Mapper = mapper;
    }
    
    public async Task<IEnumerable<ProviderDto>> GetAllProviders(CancellationToken cancellationToken)
    {
        var providers = await OfferApplicationDbContext.Providers
            .AsNoTracking()
            .Select(x => Mapper.Map<ProviderDto>(x))
            .ToListAsync(cancellationToken);

        if (providers is null)
        {
            throw new NullReferenceException();
        }

        return providers;
    }
    
    public async Task<IEnumerable<ProviderDto>> GetPopularProviders(int count, CancellationToken cancellationToken)
    {
        var popularProviders = await OfferApplicationDbContext.Providers
            .OrderByDescending(p => OfferApplicationDbContext.Offers.Count(o => o.ProviderId == p.Id))
            .Take(count)
            .Select(p => new ProviderDto
            {
                Id = p.Id,
                Name = p.Name,
                OfferCount = OfferApplicationDbContext.Offers.Count(o => o.ProviderId == p.Id)
            })
            .ToListAsync(cancellationToken);

        return popularProviders;
    }
    
    public async Task<bool> Create(ProviderDto providerDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(providerDto.Name))
        {
            throw new ArgumentException();
        }
        
        var newProvider = new ProviderEntity
        {
            Id = providerDto.Id,
            Name = providerDto.Name,
            CreatedDate = DateTimeOffset.Now
        };

        OfferApplicationDbContext.Providers.Add(newProvider);

        await OfferApplicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}