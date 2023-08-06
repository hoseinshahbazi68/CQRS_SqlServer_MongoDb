using System.Threading;
using System.Threading.Tasks;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using Data;
using MediatR;
using Sample.Core.Common.BaseChannel;
using Sample.Core.ProductApplication.Commands.AddProduct;
using Sample.Core.ProductApplication.Commands.UpdateProduct;

namespace Sample.Core.ProductApplication.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly WriteProductRepository _ProductRepository;
        private readonly WriteCatgoryRepository _CatgoryRepository;
        private readonly ApplicationDbContext _db;
        private readonly ChannelQueue<ProductUpdate> _channel;
        private readonly WriteBrandRepository _BrandRepository;


        public UpdateProductCommandHandler(WriteProductRepository ProductRepository,
            WriteCatgoryRepository            CatgoryRepository,
            WriteBrandRepository  BrandRepository,ApplicationDbContext db, ChannelQueue<ProductUpdate> channel)
        {
            _ProductRepository = ProductRepository;
            _CatgoryRepository = CatgoryRepository;
            _BrandRepository = BrandRepository;
            _db = db;
            _channel = channel;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Catgory = await _CatgoryRepository.GetCatgoryAsync(request.Catgory, cancellationToken);

            if (Catgory is null)
            {
                Catgory = new  CQRS.Entities.Write.CatgoryEntityModel { Name = request.Catgory ,IsActive=true };
                await _CatgoryRepository.AddCatgoryAsync(Catgory, cancellationToken);
            }
            var Brand = await _BrandRepository.GetBrandAsync(request.Catgory, cancellationToken);

            if (Brand is null)
            {
                Brand = new CQRS.Entities.Write.BrandEntityModel { Name = request.Catgory, IsActive = true };
                await _BrandRepository.AddBrandAsync(Brand, cancellationToken);
            }
            var Product = await _ProductRepository.Find(request.Id);

            Product.Name = request.Name;
            Product.DateModified = DateTime.Now;
            Product.Catgory = Catgory;
            Product.Brand = Brand;

            _ProductRepository.UpdateProduct(Product);

            await _db.SaveChangesAsync(cancellationToken);

            await _channel.AddToChannelAsync( new ProductUpdate() { ProductId=Product.Id }, cancellationToken);

            return true;
        }


    }
}