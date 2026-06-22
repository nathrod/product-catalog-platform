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
        public ProductCategory Category { get; set; } // e se vim nulo, não podemos já deixar setado para um valo padrão. ProductCategory.NoCategorized
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public ProductPriority Priority { get; set; }

        //E esse CreatedAt, uso como?

        //IsActive ao criar por padrão será setado para ativo, um Radio Group 
    }
}