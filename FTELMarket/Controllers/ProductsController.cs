using FTELMarket.Infrastructure;
using FTELMarket.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTELMarket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly FtelMarketDbContext _context;

    public ProductsController(FtelMarketDbContext context)
    {
        _context = context;
    }

    // GET: api/products
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Select(p => new
            {
                id = p.Id,
                name = p.Name,
                description = p.Description,
                price = p.Price,
                stock = p.StockQuantity, 
                categoryId = p.CategoryId,
                createdAt = p.CreatedAt,
                categoryName = p.Category != null ? p.Category.Name : null
            })
            .ToListAsync();

        return Ok(products);
    }

    // DTO pour recevoir les données du Front-End
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } 
        public int CategoryId { get; set; }
    }

    // POST: api/products
    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.Stock,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        await _context.Entry(product).Reference(p => p.Category).LoadAsync();

        var result = new
        {
            id = product.Id,
            name = product.Name,
            description = product.Description,
            price = product.Price,
            stock = product.StockQuantity,
            categoryId = product.CategoryId,
            createdAt = product.CreatedAt,
            categoryName = product.Category != null ? product.Category.Name : null
        };

        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, result);
    }
    // PUT: api/products/5
[HttpPut("{id}")]
public async Task<IActionResult> PutProduct(int id, [FromBody] CreateProductDto dto)
{
    var existingProduct = await _context.Products.FindAsync(id);
    if (existingProduct == null)
    {
        return NotFound(new { message = $"Le jeu avec l'ID {id} n'a pas été trouvé." });
    }

    
    var categoryExists = await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId);
    if (!categoryExists)
    {
        return BadRequest(new { message = $"La catégorie avec l'ID {dto.CategoryId} n'existe pas." });
    }

    existingProduct.Name = dto.Name;
    existingProduct.Description = dto.Description;
    existingProduct.Price = dto.Price;
    existingProduct.StockQuantity = dto.Stock;
    existingProduct.CategoryId = dto.CategoryId;

    await _context.SaveChangesAsync();

    return NoContent();
}

// DELETE: api/products/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        return NotFound(new { message = $"Le jeu avec l'ID {id} n'a pas été trouvé." });
    }

    _context.Products.Remove(product);
    await _context.SaveChangesAsync();

    return NoContent();
}
}