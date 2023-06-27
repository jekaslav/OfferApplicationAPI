namespace OfferApplication.Domain.Entities;

public class ProviderEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }
    
    public ICollection<OfferEntity> Offers { get; set; }
}