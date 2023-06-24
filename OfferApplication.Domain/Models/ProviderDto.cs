namespace OfferApplication.Domain.Models;

public class ProviderDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public DateTimeOffset CreatedTime { get; set; }
}