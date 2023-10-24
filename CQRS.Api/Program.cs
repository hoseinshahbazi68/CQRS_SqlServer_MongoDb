using CQRS.Repositories.ReadRepositories;
using CQRS.Repositories.WriteRepositories;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sample.Core.Common.BaseChannel;
using Sample.Core.Common.Pipelines;
using WebFramework;
using WebFramework.Configuration;

//var builder = new ConfigurationBuilder()
//            //.SetBasePath(env.ContentRootPath)
//            
//            .AddJsonFile($"appsettings.Development.json", optional: true)
//            .AddEnvironmentVariables();
//Configuration = builder.Build();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: false, reloadOnChange: true);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CQRS Sample",
        TermsOfService = new Uri("https://github.com/hoseinshahbazi68"),
        Contact = new OpenApiContact
        {
            Name = "Hossein Shahbazi",
            Email = "Hoseinshahbazi29@gmail.com",
            Url = new Uri("https://github.com/hoseinshahbazi68"),
        },
    });
});

builder.Services.AddMainDbContext(builder.Configuration);
builder.Services.InitMongo();
builder.Services.InitMediatR();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    app.UseSwagger();
    app.UseSwaggerUI( c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

}
app.IntializeDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

