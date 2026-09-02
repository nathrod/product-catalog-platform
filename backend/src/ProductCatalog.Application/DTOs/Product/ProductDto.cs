using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Application.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public ProductPriority Priority { get; set; }
    }
}