using Mapster;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Query;
using ProductCatalog.Application.DTOs.Sale;
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
    public class SaleHistoryService (ISqlSugarClient db, ICSVService csvService) : ISaleHistoryInterface
    {
        private readonly ISqlSugarClient _db = db;
        private readonly ICSVService _csvService = csvService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<PageListResultDto<SaleDataDto>> GetListAsync(QueryConditionDto dto, Guid productId)
        {
            List<SaleHistory> pagedSales = await _db.Queryable<SaleHistory>()
                .Where(p => p.ProductId == productId)
                // .Where(dto.Where)
                // .OrderBy(dto.OrderBy)
                .ToPageListAsync(dto.PageIndex, dto.PageSize);

            List<SaleDataDto> itemsDto = pagedSales.Adapt<List<SaleDataDto>>();

            return new PageListResultDto<SaleDataDto>()
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
        public async Task<SaleDataDto> AddSalesAsync(CreateSaleDto dto)
        {
            var productExist = await _db.Queryable<Product>().InSingleAsync(dto.ProductId) ?? throw new KeyNotFoundException("Product not found!");

            var newSales = dto.Adapt<SaleHistory>();
            var newId = Guid.NewGuid();
            newSales.Id = newId;

            await _db.Insertable(newSales).ExecuteCommandAsync();

            return newSales.Adapt<SaleDataDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="expectedProductId"></param>
        /// <returns></returns>
        public async Task<int> ProcessSalesCsvAsync(Guid expectedProductId, Stream fileStream)
        {
            var salesDtos = _csvService.ReadCSV<CreateSaleDto>(fileStream).ToList();

            if(salesDtos.Count == 0) return 0;

            bool hasInvalidProduct = salesDtos.Any(s => s.ProductId != expectedProductId);

            if(hasInvalidProduct)
            {
                throw new ArgumentException("Erro: O arquivo CSV contém dados de vendas de outros produtos. Certifique-se de enviar apenas dados do produto correto.");
            }
            
            var salesEntity = salesDtos.Adapt<List<SaleHistory>>();

            foreach (var sale in salesEntity)
            {
                if (sale.Id == Guid.Empty)
                {
                    sale.Id = Guid.NewGuid();
                }
            }
            
            int insertedCount = await _db.Insertable(salesEntity).ExecuteCommandAsync();

            return insertedCount;
        }
    }
}