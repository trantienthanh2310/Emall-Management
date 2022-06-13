using AspNetCoreSharedComponent.FileValidations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Exceptions;
using Shared.Models;
using Shared.RequestModels;
using ShopProductService.Commands.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopProductService.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IFileStorable _fileStore;

        public ProductController(IMediator mediator, IFileStorable fileStore)
        {
            _mediator = mediator;
            if (fileStore != null)
            {
                _fileStore = fileStore;
                _fileStore.SetRelationalPath("products");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ApiResult> AddProduct(
            [FromForm(Name = "requestModel")] CreateProductRequestModel requestModel)
        {
            try
            {
                requestModel.ImagePaths = await _fileStore.SaveFilesAsync(Request.Form.Files);
            }
            catch (FileValidationException ex)
            {
                return ApiResult.CreateErrorResult(400, ex.Message);
            }
            var response = await _mediator.Send(new CreateProductCommand
            {
                RequestModel = requestModel
            });
            if (!response.IsSuccess)
                return ApiResult.CreateErrorResult(500, response.ErrorMessage);
            return ApiResult<Guid>.CreateSucceedResult(response.Response);
        }

        [Authorize]
        [HttpPut("{productId}")]
        public async Task<ApiResult> EditProduct(string productId, 
            [FromForm(Name = "requestModel")] EditProductRequestModel requestModel)
        {
            try
            {
                requestModel.ImagePaths = await _fileStore.EditFilesAsync(requestModel.ImagePaths, Request.Form.Files);
            }
            catch (FileValidationException ex)
            {
                return ApiResult.CreateErrorResult(400, ex.Message);
            }
            var response = await _mediator.Send(new EditProductCommand
            {
                Id = Guid.Parse(productId),
                RequestModel = requestModel
            });
            if (!response.IsSuccess)
                return ApiResult.CreateErrorResult(500, response.ErrorMessage);
            return ApiResult<ProductDTO>.CreateSucceedResult(response.Response);
        }

        [Authorize]
        [HttpDelete("{productId}")]
        public async Task<ApiResult> DeleteProduct(string productId, [FromQuery] DeleteAction action)
        {
            var response = await _mediator.Send(new ActivateProductCommand
            {
                Id = Guid.Parse(productId),
                IsActivateCommand = action == DeleteAction.Activate,
            });
            if (!response.IsSuccess)
                return ApiResult.CreateErrorResult(500, response.ErrorMessage);
            return ApiResult<bool>.CreateSucceedResult(response.Response);
        }

        [HttpGet("shop/{shopId}/search")]
        public async Task<ApiResult> GetProductsOfShop(int shopId, [FromQuery] SearchRequestModel requestModel)
        {
            IRequest<PaginatedList<ProductDTO>> request = string.IsNullOrWhiteSpace(requestModel.Keyword)
                ? new FindProductsByShopIdQuery
                {
                    ShopId = shopId,
                    PaginationInfo = new PaginationInfo
                    {
                        PageNumber = requestModel.PageNumber,
                        PageSize = requestModel.PageSize
                    },
                    IncludeFilter = requestModel.IncludeFilter
                }
                : new FindProductsByShopIdAndKeywordQuery
                {
                    ShopId = shopId,
                    Keyword = requestModel.Keyword,
                    PaginationInfo = new PaginationInfo
                    {
                        PageNumber = requestModel.PageNumber,
                        PageSize = requestModel.PageSize
                    },
                    IncludeFilter = requestModel.IncludeFilter
                };
            var response = await _mediator.Send(request);
            return ApiResult<PaginatedList<ProductDTO>>.CreateSucceedResult(response);
        }

        [HttpGet("search")]
        public async Task<ApiResult> FindProducts([FromQuery] SearchRequestModel requestModel)
        {
            IRequest<PaginatedList<ProductDTO>> request = string.IsNullOrEmpty(requestModel.Keyword)
                ? new FindAllProductsQuery
                {
                    PaginationInfo = new PaginationInfo
                    {
                        PageNumber = requestModel.PageNumber,
                        PageSize = requestModel.PageSize
                    }
                }
                : new FindProductsByKeywordQuery
                {
                    Keyword = requestModel.Keyword,
                    PaginationInfo = new PaginationInfo
                    {
                        PageNumber = requestModel.PageNumber,
                        PageSize = requestModel.PageSize
                    }
                };
            var productList = await _mediator.Send(request);
            return ApiResult<PaginatedList<ProductDTO>>.CreateSucceedResult(productList);
        }

        [HttpGet("{productId}")]
        public async Task<ApiResult> GetSingleProduct(string productId)
        {
            var product = await _mediator.Send(new FindProductByIdQuery
            {
                Id = Guid.Parse(productId)
            });
            if (product == null)
                return ApiResult.CreateErrorResult(404, "Product is not found");
            return ApiResult<ProductWithCommentsDTO>.CreateSucceedResult((ProductWithCommentsDTO) product);
        }

        [HttpGet("less/{productId}")]
        public async Task<ApiResult> GetMinimalSingleProduct(string productId)
        {
            var product = await _mediator.Send(new FindProductByIdQuery
            {
                Id = Guid.Parse(productId),
                IsMinimal = true
            });
            if (product == null)
                return ApiResult.CreateErrorResult(404, "Product is not found");
            return ApiResult<MinimalProductDTO>.CreateSucceedResult(product);
        }

        [HttpGet("related/{productId}")]
        public async Task<ApiResult> GetRelatedProducts(string productId)
        {
            var response = await _mediator.Send(new FindRelatedProductsQuery
            {
                Id = Guid.Parse(productId)
            });
            if (!response.IsSuccess)
                return ApiResult.CreateErrorResult(404, response.ErrorMessage);
            return ApiResult<List<ProductDTO>>.CreateSucceedResult(response.Response);
        }

        [Authorize]
        [HttpPost("{productId}/import")]
        public async Task<ApiResult> ImportProduct(string productId, [FromBody] int importedQuantity)
        {
            var newQuantityResponse = await _mediator.Send(new ImportProductCommand
            {
                ProductId = Guid.Parse(productId),
                Quantity = importedQuantity
            });
            if (!newQuantityResponse.IsSuccess)
                return ApiResult.CreateErrorResult(500, newQuantityResponse.ErrorMessage);
            return ApiResult<int>.CreateSucceedResult(newQuantityResponse.Response);
        }

        [HttpGet("best")]
        public async Task<ApiResult> GetBestSellerProducts([FromQuery] int? shopId)
        {
            var bestSellerProductsResponse = await _mediator.Send(new FindBestSellerProductsQuery
            {
                ShopId = shopId
            });
            return ApiResult<List<MinimalProductDTO>>.CreateSucceedResult(bestSellerProductsResponse);
        }

        [HttpGet("category")]
        public async Task<ApiResult> GetProductsOfCategory([FromQuery(Name = "categoryId")] List<int> categoryIds, [FromQuery] SearchRequestModel requestModel, [FromQuery(Name = "direction")] OrderByDirection direction = OrderByDirection.Unspecified)
        {
            var productsOfCategoryResult = await _mediator.Send(new FindProductsByCategoryIdQuery
            {
                CategoryIds = categoryIds,
                PaginationInfo = new PaginationInfo
                {
                    PageNumber = requestModel.PageNumber,
                    PageSize = requestModel.PageSize
                },
                Keyword = requestModel.Keyword,
                Direction = direction
            });
            return ApiResult<PaginatedList<ProductDTO>>.CreateSucceedResult(productsOfCategoryResult);
        }

        [HttpGet("shop/{shopId}/category")]
        public async Task<ApiResult> GetProductsOfShopInCategory(int shopId, [FromQuery(Name = "categoryId")] List<int> categoryIds, [FromQuery] SearchRequestModel requestModel, [FromQuery(Name = "direction")] OrderByDirection direction = OrderByDirection.Unspecified)
        {
            var productsOfCategoryResult = await _mediator.Send(new FindProductsOfShopInCategoryQuery
            {
                ShopId = shopId,
                CategoryIds = categoryIds,
                PaginationInfo = new PaginationInfo
                {
                    PageNumber = requestModel.PageNumber,
                    PageSize = requestModel.PageSize
                },
                Keyword = requestModel.Keyword,
                Direction = direction
            });
            return ApiResult<PaginatedList<ProductDTO>>.CreateSucceedResult(productsOfCategoryResult);
        }

        [HttpGet("sales")]
        public async Task<ApiResult> GetTopMostSaleOffProducts()
        {
            var result = await _mediator.Send(new GetMostSaleOffProductsQuery());
            return ApiResult<List<MinimalProductDTO>>.CreateSucceedResult(result);
        }

        [HttpGet("new")]
        public async Task<ApiResult> GetTopNewProducts()
        {
            var result = await _mediator.Send(new GetTopNewProductsQuery());
            return ApiResult<List<ProductDTO>>.CreateSucceedResult(result);
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        [HttpGet("images/{image}")]
        public IActionResult GetImage(string image)
        {
            var fileResponse = _fileStore.GetFile(image);
            if (!fileResponse.IsExisted)
                return StatusCode(StatusCodes.Status404NotFound);
            return PhysicalFile(fileResponse.FullPath, fileResponse.MimeType);
        }
    }
}