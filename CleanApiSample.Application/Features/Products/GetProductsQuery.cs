using CleanApiSample.Application.Abstractions;
using CleanApiSample.Application.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Products
{
    public record GetProductsQuery() : IRequest<List<ProductDto>>;

    public class GetProductsQueryHandler: IRequestHandler<GetProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.ListAsync(cancellationToken);
            return products.Select(p => new ProductDto(
                p.Id,
                p.Code,
                p.Name,
                p.UnitPrice,
                p.Status
            )).ToList();
        }
    }
}
