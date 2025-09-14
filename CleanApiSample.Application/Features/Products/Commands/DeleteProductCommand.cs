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
    public record DeleteProductCommand(
    int Id
    ) : IRequest<bool>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.Delete(request.Id, cancellationToken);
        }
    }
}
