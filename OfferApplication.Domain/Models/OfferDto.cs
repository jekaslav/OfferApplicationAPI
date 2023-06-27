namespace OfferApplication.Domain.Models;

public class OfferDto
{
    public int Id { get; set; }
    
    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public DateTimeOffset CreatedDate { get; set; }
}