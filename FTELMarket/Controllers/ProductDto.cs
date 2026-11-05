namespace FTELMarket.Controllers;

// Le modèle pour un produit
public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } // Description optionnelle
    public decimal Price { get; set; }      // Type decimal recommandé pour les prix
    public int CategoryId { get; set; }
}

// Le modèle pour une catégorie
public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}