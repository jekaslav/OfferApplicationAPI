using Microsoft.AspNetCore.Mvc;
using OfferApplication.Domain.Models;
using OfferApplication.Services.Interfaces;

namespace OfferApplication.API.Controllers;

[ApiController]
public class ProviderController : ControllerBase
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet("providers/popular")]
    public async Task<IActionResult> GetPopularProviders(CancellationToken cancellationToken)
    {
        var popularProviders = await _providerService.GetPopularProviders(3, cancellationToken);
        return Ok(popularProviders);
    }

    [HttpPost("providers/create")]
    public async Task<IActionResult> CreateProvider([FromBody] ProviderDto providerDto, CancellationToken cancellationToken)
    {
        await _providerService.Create(providerDto, cancellationToken);
        return Ok();
    }
}