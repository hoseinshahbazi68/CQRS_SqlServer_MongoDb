using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Repositories.ReadRepositories;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Core.Common.BaseChannel;

namespace CQRS.Repositories.ProductApplication.BackgroundWorker
{
    public class AddReadModelWorker : BackgroundService
    {
        private readonly ChannelQueue<ProductAdded> _readModelChannel;
        private readonly ILogger<AddReadModelWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AddReadModelWorker(ChannelQueue<ProductAdded> readModelChannel, ILogger<AddReadModelWorker> logger, IServiceProvider serviceProvider)
        {
            _readModelChannel = readModelChannel;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var writeRepository = scope.ServiceProvider.GetRequiredService<WriteProductRepository>();
                var readProductRepository = scope.ServiceProvider.GetRequiredService<ReadProductRepository>();
                try
                {
                    await foreach (var item in _readModelChannel.ReturnValue(stoppingToken))
                    {
                        var Product = await writeRepository.GetProductByIdAsync(item.ProductId, stoppingToken);

                        if (Product != null)
                        {
                            await readProductRepository.AddAsync(new Entities.Read.ProductModel
                            {
                                ProductId = Product.Id,
                                Name = Product.Name,
                                Brand = Product.Brand,
                                Catgory = Product.Catgory,
                                IsActive = Product.IsActive,
                            }, stoppingToken);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}