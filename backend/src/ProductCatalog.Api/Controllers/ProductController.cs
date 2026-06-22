using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Products;
using ProductCatalog.Application.DTOs.Queries;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController (IProductService productService)
        {

            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PageListResultDto<ProductDto>>> GetProductList([FromQuery] QueryConditionDto dto)
        {
            var data = await _productService.GetListAsync(dto);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto dto)
        {
            var createProduct = await _productService.AddProductAsync(dto);

            return CreatedAtAction(
                "GetProductById",
                new { id = createProduct.Id },
                createProduct
            );
            // return Ok(createProduct);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] ProductDto dto)
        {
            var updatedProduct = await _productService.EditProductAsync(dto);

            return Ok(updatedProduct);
        }

        [HttpDelete]
        public async Task<ActionResult<int>> DeleteProduct([FromBody] IEnumerable<Guid> ids)
        {
            var deletedCount = await _productService.DeleteProductAsync(ids);

            if (deletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}