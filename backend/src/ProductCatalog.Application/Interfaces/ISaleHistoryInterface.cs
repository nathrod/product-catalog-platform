using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Query;
using ProductCatalog.Application.DTOs.Sale;

namespace ProductCatalog.Application.Interfaces
{
    /// <summary>
    /// Explica regra de negócio e contratos
    /// </summary>
    public interface ISaleHistoryInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<PageListResultDto<SaleDataDto>> GetListAsync(QueryConditionDto dto, Guid productId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SaleDataDto> AddSalesAsync(CreateSaleDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<int> ProcessSalesCsvAsync(Guid productId, Stream fileStream);
    }
}