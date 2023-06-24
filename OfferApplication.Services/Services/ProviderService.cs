using AutoMapper;
using OfferApplication.Domain.Contexts;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.Services.Services;

public class ProviderService : IProviderService
{
    private OfferApplicationContext OfferApplicationContext { get; }
    
    private IMapper Mapper { get; }

    public ProviderService(OfferApplicationContext context, IMapper mapper)
    {
        OfferApplicationContext = context;
        Mapper = mapper;
    }
}