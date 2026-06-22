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
    /// <param name="csvService"></param>
    public class SalesHistoryService (ISqlSugarClient db, ICSVService csvService) : ISalesHistoryInterface
    {
        private readonly ISqlSugarClient _db = db;
        private readonly ICSVService _csvService = csvService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<PageListResultDto<SalesDataDto>> GetListAsync(QueryConditionDto dto, Guid productId)
        {
            List<SalesHistory> pagedSales = await _db.Queryable<SalesHistory>()
                .Where(p => p.ProductId == productId)
                .Where(dto.Where)
                .OrderBy(dto.OrderBy)
                .ToPageListAsync(dto.PageIndex, dto.PageSize);

            List<SalesDataDto> itemsDto = pagedSales.Adapt<List<SalesDataDto>>();

            return new PageListResultDto<SalesDataDto>()
            {
                Total = itemsDto.Count,
                PageSize = dto.PageSize,
                PageIndex = dto.PageIndex,
                Items = itemsDto,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<SalesDataDto> AddSalesAsync(CreateSalesDto dto)
        {
            var productExist = await _db.Queryable<Product>().InSingleAsync(dto.ProductId) ?? throw new KeyNotFoundException("Product not found!");

            var newSales = dto.Adapt<SalesHistory>();
            var newId = Guid.NewGuid();
            newSales.Id = newId;

            await _db.Insertable(newSales).ExecuteCommandAsync();

            return newSales.Adapt<SalesDataDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="expectedProductId"></param>
        /// <returns></returns>
        public async Task<int> ProcessSalesCsvAsync(Guid expectedProductId, Stream fileStream)
        {
            var salesDtos = _csvService.ReadCSV<CreateSalesDto>(fileStream).ToList();

            if(salesDtos.Count == 0) return 0;

            bool hasInvalidProduct = salesDtos.Any(s => s.ProductId != expectedProductId);

            if(hasInvalidProduct)
            {
                throw new ArgumentException("Erro: O arquivo CSV contém dados de vendas de outros produtos. Certifique-se de enviar apenas dados do produto correto.");
            }
            
            var salesEntity = salesDtos.Adapt<List<SalesHistory>>();

            foreach (var sale in salesEntity)
            {
                if (sale.Id == Guid.Empty)
                {
                    sale.Id = Guid.NewGuid();
                }
            }
            
            int insertedCound = await _db.Insertable(salesEntity).ExecuteCommandAsync();

            return insertedCound;
        }
    }
}