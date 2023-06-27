namespace OfferApplication.Domain.Models;

public class ProviderDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public int OfferCount { get; set; }

    public DateTimeOffset CreatedDate { get; set; }
}