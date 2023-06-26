namespace OfferApplication.Domain.Entities;

public class ProviderEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public DateTimeOffset CreatedDate { get; set; }
    
    public OfferEntity Offer { get; set; }
}