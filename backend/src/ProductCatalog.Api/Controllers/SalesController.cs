using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs.Queries;
using ProductCatalog.Application.DTOs.Sales;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesHistoryInterface _salesService;

        public SalesController (ISalesHistoryInterface salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<ActionResult<SalesDataDto>> GetSalesProductList ([FromQuery] QueryConditionDto dto, Guid productId)
        {
            var data = await _salesService.GetListAsync(dto, productId);
            return Ok(data);
        }
        
        [HttpPost]
        public async Task<ActionResult<SalesDataDto>> CreateSales([FromBody] CreateSalesDto dto)
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

        [HttpPost("import-csv/{productId}")]
        public async Task<IActionResult> ImportSalesFromCsv(Guid productId, IFormFile file)
        {
            if(file == null || file.Length == 0)
                return BadRequest();
            
            if(!file.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                return BadRequest();
            
            using var stream = file.OpenReadStream();
            try
            {
                var result = await _salesService.ProcessSalesCsvAsync(productId, stream);
                return Ok(new { Message = "Vendas importadas com sucesso!", Quantidade = result });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}