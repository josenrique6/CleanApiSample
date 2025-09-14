using CleanApiSample.Application.Abstractions;
using CleanApiSample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanApiSample.Application.Features.Products.Commands
{
    public record AddProductCommand(
        int Id,
        string Codigo,
        string Nombre,
        decimal Precio
        ) :IRequest<int>;

    public class AddProductCommandHandler: IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Code = request.Codigo,
                Name = request.Nombre,
                UnitPrice = request.Precio,
                Status = true
            };
            return await _productRepository.AddAsync(product, cancellationToken);
        }
    }

}
