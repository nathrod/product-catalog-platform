namespace ProductCatalog.Application.DTOs.Sales
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSalesDto
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid ProductId{ get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime SaleDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal QuantitySold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalSaleAmount { get; set; }
    }
}