using CleanApiSample.Application.Abstractions;
using CleanApiSample.Application.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Products.Queries
{
    public record GetProductsByIdQuery(int id) : IRequest<ProductDto?>;
    public class GetProductsByIdQueryHandler: IRequestHandler<GetProductsByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _productRepository;
        public GetProductsByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.id, cancellationToken);
            if (product == null) return null;
            return new ProductDto(product.Id, product.Code, product.Name, product.UnitPrice, product.Status);
        }
    }
}
