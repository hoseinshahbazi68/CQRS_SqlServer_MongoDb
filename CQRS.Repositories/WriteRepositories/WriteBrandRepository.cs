using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Read;
using CQRS.Entities.Write;
using Data;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Repositories.WriteRepositories
{
    public class WriteBrandRepository
    {
        private readonly ApplicationDbContext _db;

        public WriteBrandRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<BrandEntityModel> GetBrandAsync(string name, CancellationToken cancellationToken = default)
        {
            return _db.Brand.FirstOrDefaultAsync(d => d.Name == name, cancellationToken: cancellationToken);
        }

        public async Task AddBrandAsync(BrandEntityModel Brand, CancellationToken cancellationToken = default)
        {
            await _db.Brand.AddAsync(Brand, cancellationToken);
        }
    }
}