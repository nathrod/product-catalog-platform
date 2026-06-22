using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Products;
using ProductCatalog.Application.DTOs.Queries;

namespace ProductCatalog.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageListResultDto<ProductDto>> GetListAsync(QueryConditionDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDetailsDto> GetProductByIdAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ProductDto> AddProductAsync(CreateProductDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ProductDto> EditProductAsync(ProductDto dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> DeleteProductAsync(IEnumerable<Guid> ids);
    }
}