using ProductCatalog.Application.DTOs.Sales;
using ProductCatalog.Application.DTOs.Queries;

namespace ProductCatalog.Application.DTOs.Products
{
    public class QueryProductsDto
    {
        public GetSalesDataDto GetSalesDataQueryDto { get; set; }

        public QueryConditionDto QueryConditionDto { get; set; }
    }
}