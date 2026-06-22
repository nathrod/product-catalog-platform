using Mapster;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Queries;
using ProductCatalog.Application.DTOs.Sales;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using SqlSugar;

namespace ProductCatalog.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public class SalesHistoryService (ISqlSugarClient db) : ISalesHistoryInterface
    {
        private readonly ISqlSugarClient _db = db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageListResultDto<SalesDataDto>> GetListAsync(QueryConditionDto dto)
        {
            RefAsync<int> total = 0;

            List<SalesHistory> pagedSales = await _db.Queryable<SalesHistory>()
                .Where(dto.Where)
                .OrderBy(dto.OrderBy)
                .ToPageListAsync(dto.PageIndex, dto.PageSize);

            List<SalesDataDto> itemsDto = pagedSales.Adapt<List<SalesDataDto>>();

            return new PageListResultDto<SalesDataDto>()
            {
                Total = total,
                PageSize = dto.PageSize,
                PageIndex = dto.PageIndex,
                Items = itemsDto,
            };
        }
    }
}