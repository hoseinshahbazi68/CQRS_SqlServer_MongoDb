using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Write;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using CQRS.Service.ProductApplication.Commands.AddProduct.Model;
using Data;
using MediatR;
using Sample.Core.Common.BaseChannel;

namespace Sample.Core.ProductApplication.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductCommandResult>
    {
        private readonly WriteProductRepository _ProductRepository;
        private readonly WriteCatgoryRepository _CatgoryRepository;
        private readonly WriteBrandRepository _BrandRepository;
        private readonly ApplicationDbContext _db;
        private readonly ChannelQueue<ProductAdded> _channel;


        public AddProductCommandHandler(WriteProductRepository ProductRepository, WriteCatgoryRepository CatgoryRepository
            , WriteBrandRepository  BrandRepository, 
            ApplicationDbContext db, ChannelQueue<ProductAdded> channel)
        {
            _ProductRepository = ProductRepository;
            _CatgoryRepository = CatgoryRepository;
            _BrandRepository = BrandRepository;
            _db = db;
            _channel = channel;
        }

        public async Task<AddProductCommandResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var Catgory = await _CatgoryRepository.GetCatgoryAsync(request.Catgory, cancellationToken);

            if (Catgory is null)
            {
                Catgory = new CQRS.Entities.Write.CatgoryEntityModel { Name = request.Catgory, IsActive = true };
                await _CatgoryRepository.AddCatgoryAsync(Catgory, cancellationToken);
            }

            var Brand = await _BrandRepository.GetBrandAsync(request.Catgory, cancellationToken);

            if (Brand is null)
            {
                Brand = new CQRS.Entities.Write.BrandEntityModel { Name = request.Catgory, IsActive = true };
                await _BrandRepository.AddBrandAsync(Brand, cancellationToken);
            }


            var Product = new ProductEntityModel
            {
                Name = request.Name,
                IsActive = request.IsActive,
                Catgory = Catgory,
                 Brand= Brand,
            };

            await _ProductRepository.AddProductAsync(Product, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            await _channel.AddToChannelAsync(new ProductAdded { ProductId = Product.Id }, cancellationToken);

            return new AddProductCommandResult { ProductId = Product.Id };
        }
    }
}