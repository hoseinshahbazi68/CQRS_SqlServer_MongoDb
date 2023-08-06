using CQRS.Entities.Read;
using CQRS.Repositories.ReadRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Core.ProductApplication.Queries.GetProductAll
{
    public  class GetProductQueryHandler : IRequestHandler<GetProductAll, List<ProductModel>>
    {
        private readonly ReadProductRepository _readProductRepository;

        public GetProductQueryHandler(ReadProductRepository readProductRepository)
        {
            _readProductRepository = readProductRepository;
        }

        public Task<List<ProductModel>> Handle(GetProductAll request, CancellationToken cancellationToken)
        {
            return _readProductRepository.GetAllAsync( cancellationToken);
        }
    }
}