using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Product;
using ProductCatalog.Application.DTOs.Query;

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
        Task<ProductDto> GetProductByIdAsync(Guid id);
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
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="imageContentType"></param>
        /// <returns></returns>
        Task<ProductDto> EditProductAsync(ProductDto dto, Stream? imageStream = null, string? imageFileName = null, string? imageContentType = null);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> DeleteProductAsync(IEnumerable<Guid> ids);
    }
}