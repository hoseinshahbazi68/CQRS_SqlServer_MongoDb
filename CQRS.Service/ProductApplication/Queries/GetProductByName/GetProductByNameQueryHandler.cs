using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Read;
using CQRS.Repositories.ReadRepositories;
using MediatR;

namespace Sample.Core.ProductApplication.Queries.GetProductByName
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, ProductModel>
    {
        private readonly ReadProductRepository _readProductRepository;

        public GetProductByNameQueryHandler(ReadProductRepository readProductRepository)
        {
            _readProductRepository = readProductRepository;
        }

        public Task<ProductModel> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            return _readProductRepository.GetByNameAsync(request.ProductName, cancellationToken);
        }
    }
}