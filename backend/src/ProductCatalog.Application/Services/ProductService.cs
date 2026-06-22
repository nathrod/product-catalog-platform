using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using SqlSugar;
using Mapster;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Products;
using ProductCatalog.Application.DTOs.Queries;

namespace ProductCatalog.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    public class ProductService(ISqlSugarClient db) : IProductService
    {
        private readonly ISqlSugarClient _db = db;

        private async Task ValidateRequiredFields(Product product)
        {
            var productCodeExists = await _db.Queryable<Product>().AnyAsync(p => p.Code == product.Code && p.Id != product.Id);
            if (productCodeExists)
            {
                // throw new CustomException(PostgresErrorCodes.)
                Console.WriteLine("Code already exist!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageListResultDto<ProductDto>> GetListAsync(QueryConditionDto dto)
        {
            RefAsync<int> total = 0;
            
            List<Product> pagedProducts = await _db.Queryable<Product>()
                .Where(dto.Where)
                .OrderBy(dto.OrderBy)
                .ToPageListAsync(dto.PageIndex, dto.PageSize);
            
            List<ProductDto> itemsDto = pagedProducts.Adapt<List<ProductDto>>();
            
            return new PageListResultDto<ProductDto>()
            {
                Total = total,
                PageSize = dto.PageSize,
                PageIndex = dto.PageIndex,
                Items = itemsDto,
            };
        }

        // GET /api/todoitems/{id} Para acessar a pagina daquele produto
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductDetailsDto> GetProductByIdAsync(Guid id)
        {
            var prodcutDetails = await _db.Queryable<Product>().InSingleAsync(id);

            return prodcutDetails.Adapt<ProductDetailsDto>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ProductDto> AddProductAsync(CreateProductDto dto)
        {
            var newProduct = dto.Adapt<Product>();
            var newId = Guid.NewGuid();
            newProduct.Id = newId;
            
            await ValidateRequiredFields(newProduct);
            await _db.Insertable(newProduct).ExecuteCommandAsync();

            return newProduct.Adapt<ProductDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ProductDto> EditProductAsync(ProductDto dto)
        {
            var editProduct = dto.Adapt<Product>();
            await ValidateRequiredFields(editProduct);

            await _db.Updateable(editProduct).ExecuteCommandAsync();

            return editProduct.Adapt<ProductDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<int> DeleteProductAsync(IEnumerable<Guid> ids)
        {
            return await _db.Deleteable<Product>()
                .In(ids)
                .ExecuteCommandAsync();
        }
        
    }
}