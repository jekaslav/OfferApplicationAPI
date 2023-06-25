namespace OfferApplication.Domain.Models;

public class OfferDto
{
    public int Id { get; set; }
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public DateTimeOffset CreatedDate { get; set; }
}