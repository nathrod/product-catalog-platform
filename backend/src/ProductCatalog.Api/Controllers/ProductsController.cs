using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Api.Models.Products;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Product;
using ProductCatalog.Application.DTOs.Query;
using ProductCatalog.Application.Interfaces;

namespace ProductCatalog.Api.Controllers
{
    /// <summary>
    /// Provides endpoints for managing the products
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductsController (IProductService productsService)
        {
            _productsService = productsService;
        }

        /// <summary>
        /// Retrieves a paginated list of products
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ActionResult<PageListResultDto<ProductDto>>> GetProductList([FromBody] QueryConditionDto dto)
        {
            
            var data = await _productsService.GetListAsync(dto);
            return Ok(data);
        }

        /// <summary>
        /// Retrieves a product by its unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailsDto>> GetProductById(Guid id)
        {
            var product = await _productsService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromForm] CreateProductRequest request)
        {
            try
            {
                var dto = new CreateProductDto
                {
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
                    Category = request.Category,
                    Price = request.Price,
                    IsActive = request.IsActive,
                    Priority = request.Priority,
                    Image = request.Image != null
                        ? new ImageFileDto
                        {
                            ImageStream = request.Image.OpenReadStream(),
                            ImageFileName = request.Image.FileName,
                            ImageContentType = request.Image.ContentType
                        }
                        : null
                };

                var createdProduct =
                    await _productsService.AddProductAsync(dto);

                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = createdProduct.Id },
                    createdProduct
                );
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] ProductDto dto)
        {
            try
            {
                var updatedProduct = await _productsService.EditProductAsync(dto);

                return Ok(updatedProduct);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message); 
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes one or more products
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteProduct([FromBody] IEnumerable<Guid> ids)
        {
            var deletedCount = await _productsService.DeleteProductAsync(ids);

            if (deletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}