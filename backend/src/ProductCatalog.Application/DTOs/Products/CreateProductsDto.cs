using System.ComponentModel.DataAnnotations;
using ProductCatalog.Domain.Enums;

namespace ProductCatalog.Application.DTOs.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product Code is required")]
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public ProductPriority Priority { get; set; }
    }
}