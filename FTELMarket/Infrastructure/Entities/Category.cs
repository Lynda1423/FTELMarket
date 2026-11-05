namespace FTELMarket.Infrastructure.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Relation : Une catégorie contient plusieurs produits (1-à-plusieurs)[cite: 3]
    public ICollection<Product> Products { get; set; } = new List<Product>();
}