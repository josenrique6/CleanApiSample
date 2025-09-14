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
    public record UpdateProductoCommand(
        int Id,
        string Codigo,
        string Nombre,
        decimal Precio,
        bool Estado
        ) : IRequest<bool>;

    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductoCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = request.Id,
                Code = request.Codigo,
                Name = request.Nombre,
                UnitPrice = request.Precio,
                Status = request.Estado
            };
            return await _productRepository.Update(product, cancellationToken);
        }
    }
}
