using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Query;
using ProductCatalog.Application.DTOs.Sale;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Api.Controllers
{
    /// <summary>
    /// Manages operations related to product sales
    /// allowing for the consultation, registration, and import of sales via CSV file
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleHistoryInterface _salesService;

        public SalesController (ISaleHistoryInterface salesService)
        {
            _salesService = salesService;
        }

        /// <summary>
        /// Retrieves a product's sales list
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PageListResultDto<SaleDataDto>>> GetSalesProductList ([FromQuery] QueryConditionDto dto, Guid productId)
        {
            var data = await _salesService.GetListAsync(dto, productId);
            return Ok(data);
        }

        /// <summary>
        /// Create a new sales record
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SaleDataDto>> CreateSales([FromBody] CreateSaleDto dto)
        {
            try
            {
                var salesCreated = await _salesService.AddSalesAsync(dto);
                return Ok(salesCreated);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Imports sales data for a product from a CSV file
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import-csv/{productId}")]
        public async Task<ActionResult<int>> ImportSalesFromCsv(Guid productId, IFormFile file)
        {
            if(file == null || file.Length == 0)
                return BadRequest();
            
            if(!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest();
            
            using var stream = file.OpenReadStream();

            try
            {
                var result = await _salesService.ProcessSalesCsvAsync(productId, stream);
                return Ok(result);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}