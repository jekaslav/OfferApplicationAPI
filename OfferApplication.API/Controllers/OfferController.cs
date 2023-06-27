using Microsoft.AspNetCore.Mvc;
using OfferApplication.Domain.Models;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.API.Controllers;
[ApiController]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpPost("offers/create")]
    public async Task<IActionResult> CreateOffer([FromBody] OfferDto offerDto, int providerId)
    {
        await _offerService.Create(offerDto, providerId, CancellationToken.None);
        return Ok();
    }

    [HttpGet("offers/search")]
    public async Task<IActionResult> SearchOffers(string? brand, string? model = null, string? provider = null)
    {
        var offers = await _offerService.SearchOffers(brand, model, provider, CancellationToken.None);
        return Ok(offers);
    }
}