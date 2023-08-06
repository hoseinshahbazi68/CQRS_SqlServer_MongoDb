using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Write;
using CQRS.Models.ViewModel;
using Data;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Repositories.WriteRepositories
{
    public class WriteProductRepository
    {
        private readonly ApplicationDbContext _db;

        public WriteProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddProductAsync(ProductEntityModel Product, CancellationToken cancellationToken = default)
        {
            await _db.Product.AddAsync(Product, cancellationToken);
        }

        public void UpdateProduct(ProductEntityModel Product)
        {
            _db.Entry(Product).State = EntityState.Modified;
        }

        public Task<ProductEntityModel> Find(int ProductId, CancellationToken cancellationToken = default)
        {
            return _db.Product.OrderBy(x => x.Id).LastOrDefaultAsync(c => c.Id == ProductId, cancellationToken);
        }
        public Task<ProductViewModel> GetProductByIdAsync(int ProductId, CancellationToken cancellationToken = default)
        {
            return _db.Product.Select(x => new ProductViewModel()
            {
                Brand = x.Brand.Name,
                Catgory = x.Catgory.Name,
                Id = x.Id,
                BrandId = x.BrandId,
                Name = x.Name,
                IsActive = x.IsActive,
                CatgoryId = x.CatgoryId,
            }).FirstOrDefaultAsync(c => c.Id == ProductId, cancellationToken);
        }
        public void DeleteProduct(ProductEntityModel Product)
        {
            _db.Product.Remove(Product);
        }
    }
}