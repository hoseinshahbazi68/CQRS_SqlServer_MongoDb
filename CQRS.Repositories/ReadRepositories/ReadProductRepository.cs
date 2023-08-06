using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Read;
using CQRS.Repositories.ReadRepositories.Common;
using MongoDB.Driver;

namespace CQRS.Repositories.ReadRepositories
{
    public class ReadProductRepository : BaseReadRepository<ProductModel>
    {
        public ReadProductRepository(IMongoDatabase db) : base(db)
        {
        }

        public Task<ProductModel> GetByProductIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(product => product.ProductId == id, cancellationToken);
        }

        public Task<ProductModel> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return base.FirstOrDefaultAsync(Product => Product.Name == name, cancellationToken);
        }
        public new Task<List<ProductModel>> GetAllAsync( CancellationToken cancellationToken = default)
        {
            return base.GetAllAsync(cancellationToken);
        }

        public Task DeleteByProductIdAsync(int ProductId, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(m => m.ProductId == ProductId, cancellationToken);
        }
    }
}