using AutoMapper;
using OfferApplication.Domain.Entities;
using OfferApplication.Domain.Models;

namespace OfferApplication.Services.Mappers;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<OfferEntity, OfferDto>();
    }
}