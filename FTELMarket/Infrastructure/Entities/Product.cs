namespace FTELMarket.Infrastructure.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } // Facultative[cite: 3]
    public decimal Price { get; set; }       // Type decimal pour l'argent[cite: 4]
    public int StockQuantity { get; set; }   // Quantité en stock[cite: 3]
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow; // Horodatage UTC[cite: 3, 4]

    // Clé étrangère vers la catégorie[cite: 3]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}