﻿using Refit;
using Shared.DTOs;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Clients
{
    public interface IProductClient
    {
        [Get("/products/search?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<ApiResponse<ApiResult<PaginatedList<ProductDTO>>>> GetProductsAsync(int pageNumber, int? pageSize);

        [Get("/products/{productId}")]
        Task<ApiResponse<ApiResult<ProductWithCommentsDTO>>> GetProductAsync(string productId);

        [Get("/products/less/{productId}")]
        Task<ApiResponse<ApiResult<ProductDTO>>> GetProductInfoInCheckout(string productId);

        [Get("/products/shop/{shopId}/search?pageSize=0")]
        Task<ApiResponse<ApiResult<PaginatedList<ProductDTO>>>> GetProductsOfShopAsync(int shopId);

        [Get("/products/search?keyword={keyword}&pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<ApiResponse<ApiResult<PaginatedList<ProductDTO>>>> FindProducts(string keyword, int pageNumber, int? pageSize);

        [Get("/products/best")]
        Task<ApiResponse<ApiResult<List<MinimalProductDTO>>>> GetBestSellerProducts([Query] int? shopId);

        [Get("/products/shop/{shopId}/category?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<ApiResponse<ApiResult<PaginatedList<ProductDTO>>>> GetProductsOfShopInCategory(int shopId, [Query(CollectionFormat.Multi)] int[] categoryId, [Query] string keyword, int pageNumber, int pageSize);

        [Get("/products/category?pageNumber={pageNumber}&pageSize={pageSize}")]
        Task<ApiResponse<ApiResult<PaginatedList<ProductDTO>>>> GetProductsInCategory([Query(CollectionFormat.Multi)] int[] categoryId, [Query] string keyword, [Query] OrderByDirection direction, int pageNumber, int pageSize);

        [Get("/products/sales")]
        Task<ApiResponse<ApiResult<List<MinimalProductDTO>>>> GetMostSaleOffProducts();

        [Get("/products/new")]
        Task<ApiResponse<ApiResult<List<ProductDTO>>>> GetTopNewsProducts();
    }
}