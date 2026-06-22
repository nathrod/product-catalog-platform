using ProductCatalog.Domain.Enums;
using SqlSugar;

//Normalmente essas entidades herdam de uma entidade base, Base Entity

namespace ProductCatalog.Domain.Entities
{
    [SugarTable("product")]
    public class Product
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "uuid")]
        public Guid Id { get; set; }

        [SugarColumn(Length = 20, IsNullable = false)]
        public string Code { get; set; } = string.Empty;

        [SugarColumn(Length = 100, IsNullable = false)]
        public string Name { get; set; } = string.Empty;

        [SugarColumn(Length = 250)]
        public string? Description { get; set; } 

        [SugarColumn(IsNullable = false)]
        public ProductCategory Category { get; set; }
        
        [SugarColumn(ColumnDataType = "numeric(12,2)", IsNullable = false)]
        public decimal Price { get; set; }

        [SugarColumn(IsNullable = false)]
        public bool IsActive { get; set; } //o Frontend saber se deve mostrar o botão "Comprar" ou o texto "Produto Esgotado" when user see the get by id page. On the forms this will be a radio button active disabled

        [SugarColumn(IsNullable = false)]
        public ProductPriority Priority { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        public decimal Rating { get; set; }

        public int ReviewCount { get; set; }

        // [SugarColumn(IsNullable = false)]
        // public DateTime CreatedAt { get; set; }
    }
}