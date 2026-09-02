using SqlSugar;

namespace ProductCatalog.Domain.Entities
{
    [SugarTable("product_sales")]
    public class SaleHistory
    {
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [SugarColumn(IsNullable = false)]
        public Guid ProductId{ get; set; }

        public DateTime SaleDate { get; set; }

        public decimal QuantitySold { get; set; }

        [SugarColumn(ColumnDataType = "numeric(12,2)")]
        public decimal TotalSaleAmount { get; set; }
    }
}