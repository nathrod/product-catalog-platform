using ProductCatalog.Application.Interfaces;
using ProductCatalog.Domain.Entities;
using SqlSugar;
using Mapster;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.DTOs.Product;
using ProductCatalog.Application.DTOs.Query;

namespace ProductCatalog.Application.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="imageStorageService"></param>
    public class ProductService(ISqlSugarClient db, IImageStorageService imageStorageService) : IProductService
    {
        private readonly ISqlSugarClient _db = db;
        private readonly IImageStorageService _imageStorageService = imageStorageService;

        private async Task ValidateRequiredFields(Product product)
        {
            var productCodeExists = await _db.Queryable<Product>().AnyAsync(p => p.Code == product.Code && p.Id != product.Id);
            if (productCodeExists)
            {
                throw new ArgumentException("Code already exist!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageListResultDto<ProductDto>> GetListAsync(QueryConditionDto dto)
        {   
            RefAsync<int> totalCount = 0;

            var query = _db.Queryable<Product>();
            
            foreach (var filter in dto.Filters)
            {
                if (string.IsNullOrWhiteSpace(filter.FieldName))
                {
                    continue;
                }

                switch (filter.FieldName.ToLower())
                {
                    case "code":
                        query = query.Where(p=>
                            p.Code.Contains(filter.FieldValue));
                        break;
                    case "name":
                        query = query.Where(p =>
                            p.Name.Contains(filter.FieldValue));
                        break;
                    case "price":
                        if (decimal.TryParse(
                            filter.FieldValue,
                            out var price))
                        {
                            query = query.Where(p =>
                                p.Price == price);
                        }
                        break;
                    case "category":
                        if (int.TryParse(
                            filter.FieldValue,
                            out var category))
                        {
                            query = query.Where(p =>
                                (int)p.Category == category);
                        }
                        break;
                    case "isactive":
                        if (bool.TryParse(
                            filter.FieldValue,
                            out var isActive))
                        {
                            query = query.Where(p =>
                                p.IsActive == isActive);
                        }
                        break;
                }
            }

            foreach (var sort in dto.Sorts)
            {
                switch (sort.FieldName.ToLower())
                {
                    case "price":
                        query = sort.Descending
                            ? query.OrderBy(p => p.Price, OrderByType.Desc)
                            : query.OrderBy(p => p.Price, OrderByType.Asc);
                        break;
                    case "name":
                        query = sort.Descending
                            ? query.OrderBy(p => p.Name, OrderByType.Desc)
                            : query.OrderBy(p => p.Name, OrderByType.Asc);
                        break;
                    case "code":
                        query = sort.Descending
                            ? query.OrderBy(p => p.Code, OrderByType.Desc)
                            : query.OrderBy(p => p.Code, OrderByType.Asc);
                        break;
                    case "priority":
                        query = sort.Descending
                            ? query.OrderBy(p => p.Priority, OrderByType.Desc)
                            : query.OrderBy(p => p.Priority, OrderByType.Asc);
                        break;
                    case "category":
                        query = sort.Descending
                            ? query.OrderBy(p => p.Category, OrderByType.Desc)
                            : query.OrderBy(p => p.Category, OrderByType.Asc);
                        break;
                }
            }

            List<Product> pagedProducts = await query.ToPageListAsync(dto.PageIndex, dto.PageSize, totalCount);

            // List<Product> pagedProducts = await _db.Queryable<Product>()
            //     // .Where(dto.Where)
            //     // .OrderBy(dto.OrderBy)
            //     .ToPageListAsync(dto.PageIndex, dto.PageSize, totalCount);
            
            List<ProductDto> itemsDto = pagedProducts.Adapt<List<ProductDto>>();
            
            return new PageListResultDto<ProductDto>()
            {
                Total = totalCount,
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
        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var product = await _db.Queryable<Product>().InSingleAsync(id);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found!");
            }

            return product.Adapt<ProductDto>();
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

            if (dto.Image != null)
            {
                var uploadResult =
                    await _imageStorageService.UploadAsync(
                        dto.Image.ImageStream,
                        dto.Image.ImageFileName,
                        dto.Image.ImageContentType
                    );

                newProduct.ImageURL = uploadResult.Url;
                newProduct.ImagePublicId = uploadResult.PublicId;
            }
            
            await ValidateRequiredFields(newProduct);

            await _db.Insertable(newProduct).ExecuteCommandAsync();

            return newProduct.Adapt<ProductDto>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="imageStream"></param>
        /// <param name="imageFileName"></param>
        /// <param name="imageContentType"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<ProductDto> EditProductAsync(ProductDto dto, Stream? imageStream = null, string? imageFileName = null, string? imageContentType = null)
        {
            var productExists = await _db.Queryable<Product>().InSingleAsync(dto.Id) ?? throw new KeyNotFoundException("Product not found!");

            dto.Adapt(productExists);

            if (imageStream != null)
            {
                var uploadResult =
                    await _imageStorageService.UploadAsync(
                        imageStream,
                        "products",
                        imageFileName ?? "product-image"
                    );

                productExists.ImageURL = uploadResult.Url;
                productExists.ImagePublicId =
                    uploadResult.PublicId;
            }

            await ValidateRequiredFields(productExists);

            await _db.Updateable(productExists).ExecuteCommandAsync();

            return productExists.Adapt<ProductDto>();
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