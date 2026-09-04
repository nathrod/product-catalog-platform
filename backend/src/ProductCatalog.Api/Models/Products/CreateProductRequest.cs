using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Api.Models.Products
{
    public class CreateProductRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public ProductPriority Priority { get; set; }
        public IFormFile? Image { get; set; }
    }
}