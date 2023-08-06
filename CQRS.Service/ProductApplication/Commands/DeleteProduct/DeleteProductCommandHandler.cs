using System.Threading;
using System.Threading.Tasks;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using Data;
using MediatR;
using Sample.Core.Common.BaseChannel;

namespace Sample.Core.ProductApplication.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommand,bool>
   {
       private readonly WriteProductRepository _writeProductRepository;
       private readonly ApplicationDbContext _db;
       private readonly ChannelQueue<ProductDeleted> _channelQueue;

       public DeleteProductCommandHandler(WriteProductRepository writeProductRepository, ApplicationDbContext db, ChannelQueue<ProductDeleted> channelQueue)
       {
           _writeProductRepository = writeProductRepository;
           _db = db;
           _channelQueue = channelQueue;
       }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await _writeProductRepository.Find(request.ProductId, cancellationToken);

            if (Product is null)
                return false;

            _writeProductRepository.DeleteProduct(Product);

            await _db.SaveChangesAsync(cancellationToken);

            await _channelQueue.AddToChannelAsync(new ProductDeleted { ProductId = request.ProductId }, cancellationToken);


            return true;
        }
    }
}