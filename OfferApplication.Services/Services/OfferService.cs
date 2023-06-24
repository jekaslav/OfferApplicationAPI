using AutoMapper;
using OfferApplication.Domain.Contexts;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.Services.Services;

public class OfferService : IOfferService
{
    private OfferApplicationContext OfferApplicationContext { get; }
    
    private IMapper Mapper { get; }

    public OfferService(OfferApplicationContext context, IMapper mapper)
    {
        OfferApplicationContext = context;
        Mapper = mapper;
    }
    
    
}