namespace OfferApplication.Domain.Entities;

public class OfferEntity
{
    public int Id { get; set; }
    
    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }
    
    public int ProviderId { get; set; }
    
    public ProviderEntity Provider { get; set; }
}