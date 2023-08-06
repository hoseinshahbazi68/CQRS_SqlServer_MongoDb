using CQRS.Entities.Read;
using MediatR;

namespace Sample.Core.ProductApplication.Queries.GetProductByName
{
    public class GetProductByNameQuery : IRequest<ProductModel>
    {
        public string ProductName { get; set; }
    }
}