using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Queries;
using ProductCatalog.Application.DTOs.Sales;

namespace ProductCatalog.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISalesHistoryInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageListResultDto<SalesDataDto>> GetListAsync(QueryConditionDto dto);
    }
}