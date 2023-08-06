using MediatR;

namespace Sample.Core.ProductApplication.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
    }
}