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
    public async Task<IActionResult> CreateOffer([FromBody] OfferDto offerDto, int providerId, CancellationToken cancellationToken)
    {
        await _offerService.Create(offerDto, providerId, cancellationToken);
        return Ok();
    }

    [HttpGet("offers/search")]
    public async Task<IActionResult> SearchOffers([FromQuery] string? brand, [FromQuery] string? model, [FromQuery] string? provider, CancellationToken cancellationToken)
    {
        var offers = await _offerService.SearchOffers(brand, model, provider, cancellationToken);
        return Ok(offers);
    }
}