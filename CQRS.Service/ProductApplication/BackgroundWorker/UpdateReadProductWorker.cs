using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Read;
using CQRS.Repositories.ReadRepositories;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Core.Common.BaseChannel;

namespace Sample.Core.ProductApplication.BackgroundWorker
{
    public class UpdateReadProductWorker : BackgroundService
    {
        private readonly ChannelQueue<ProductUpdate> _readModelChannel;
        private readonly ILogger<UpdateReadProductWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateReadProductWorker(ChannelQueue<ProductUpdate> readModelChannel, ILogger<UpdateReadProductWorker> logger, IServiceProvider serviceProvider)
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
                            System.Linq.Expressions.Expression<Func<ProductModel, bool>> expr = i => i.ProductId== Product.Id;

                            await readProductRepository.UpdateAsync(new  CQRS.Entities.Read.ProductModel
                            {
                                ProductId = Product.Id,
                                Name = Product.Name,
                            }, expr, stoppingToken);
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