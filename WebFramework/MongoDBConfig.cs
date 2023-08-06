using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MongoDBConfig
{
    public static void InitMongo(this IServiceCollection services)
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var mongoDatabase = mongoClient.GetDatabase("CQRSDB");
       // services.AddSingleton<ICaptchaRepositories, CaptchaRepositories>();
        services.AddSingleton(mongoDatabase);
    }
}
