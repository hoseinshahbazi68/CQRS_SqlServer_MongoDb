using System;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Repositories.ReadRepositories;
using CQRS.Service.ProductApplication.BackgroundWorker.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Core.Common.BaseChannel;

namespace CQRS.Repositories.ProductApplication.BackgroundWorker
{
    public class DeleteReadProductWorker : BackgroundService
    {

        private readonly ChannelQueue<ProductDeleted> _deleteModelChannel;
        private readonly ILogger<DeleteReadProductWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DeleteReadProductWorker(ChannelQueue<ProductDeleted> deleteModelChannel, ILogger<DeleteReadProductWorker> logger, IServiceProvider serviceProvider)
        {
            _deleteModelChannel = deleteModelChannel;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var readProductRepository = scope.ServiceProvider.GetRequiredService<ReadProductRepository>();


                    await foreach (var item in _deleteModelChannel.ReturnValue(stoppingToken))
                    {
                        await readProductRepository.DeleteByProductIdAsync(item.ProductId, stoppingToken);
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