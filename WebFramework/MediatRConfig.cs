using CQRS.Repositories.ProductApplication.BackgroundWorker;
using CQRS.Repositories.ReadRepositories;
using CQRS.Repositories.WriteRepositories;
using CQRS.Service.ProductApplication.Commands.AddProduct.Model;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Sample.Core.Common.BaseChannel;
using Sample.Core.Common.Pipelines;
using Sample.Core.ProductApplication.BackgroundWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace WebFramework
{
    static public class MediatRConfig
    { 
        public static void InitMediatR(this IServiceCollection Services)
        {
            Services.AddScoped<WriteProductRepository>();
            Services.AddScoped<WriteBrandRepository>();
            Services.AddScoped<WriteCatgoryRepository>();

            Services.AddSingleton(typeof(ChannelQueue<>));




            Services.AddScoped<ReadProductRepository>();
            Services.AddScoped<ReadProductRepository>();

            Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<AddProductCommand>());

            Services.AddHostedService<AddReadModelWorker>();
            Services.AddHostedService<DeleteReadProductWorker>();
            Services.AddHostedService<UpdateReadProductWorker>();
        }
    }
}
