namespace ProductCatalog.Application.DTOs.Sale
{
    public class SaleDataDto
    {
        public Guid Id { get; set; }
        public Guid ProductId{ get; set; }
        public DateTime SaleDate { get; set; }
        public decimal QuantitySold { get; set; }
        public decimal TotalSaleAmount { get; set; }
    }
}