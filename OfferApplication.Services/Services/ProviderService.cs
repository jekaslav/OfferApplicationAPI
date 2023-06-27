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

    public ProviderService(OfferApplicationDbContext context, IMapper mapper)
    {
        OfferApplicationDbContext = context;
    }

    public async Task<IEnumerable<ProviderDto>> GetPopularProviders(int count, CancellationToken cancellationToken)
    {
        var popularProviders = await OfferApplicationDbContext.Providers
            .OrderByDescending(p => OfferApplicationDbContext.Offers.Count(o => o.ProviderId == p.Id))
            .Take(count)
            .ToListAsync(cancellationToken);

        var providerDtos = popularProviders.Select(p => new ProviderDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedDate = p.CreatedDate
        }).ToList();

        var providerIds = providerDtos.Select(p => p.Id).ToList();
    
        var offerCounts = await OfferApplicationDbContext.Offers
            .Where(x => providerIds.Contains(x.ProviderId))
            .GroupBy(x => x.ProviderId)
            .Select(x => new { ProviderId = x.Key, OfferCount = x.Count() })
            .ToListAsync(cancellationToken);

        foreach (var providerDto in providerDtos)
        {
            providerDto.OfferCount = offerCounts.FirstOrDefault(c => c.ProviderId == providerDto.Id)?.OfferCount ?? 0;
        }

        return providerDtos;
    }
    
    public async Task<bool> Create(ProviderDto providerDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(providerDto.Name))
        {
            throw new ArgumentException();
        }

        var newProvider = new ProviderEntity
        {
            Name = providerDto.Name,
            CreatedDate = DateTimeOffset.Now
        };

        OfferApplicationDbContext.Providers.Add(newProvider);

        await OfferApplicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}