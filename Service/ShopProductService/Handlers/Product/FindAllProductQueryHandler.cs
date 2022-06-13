﻿using DatabaseAccessor.Repositories.Abstraction;
using MediatR;
using Shared.DTOs;
using Shared.Models;
using ShopProductService.Commands.Product;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopProductService.Handlers.Product
{
    public class FindAllProductQueryHandler : 
        IRequestHandler<FindAllProductsQuery, PaginatedList<ProductDTO>>, IDisposable
    {
        private readonly IProductRepository _repository;

        public FindAllProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<ProductDTO>> Handle(FindAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllProductAsync(request.PaginationInfo);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
