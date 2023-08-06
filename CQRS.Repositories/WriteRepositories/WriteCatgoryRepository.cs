using System.Threading;
using System.Threading.Tasks;
using CQRS.Entities.Read;
using CQRS.Entities.Write;
using Data;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Repositories.WriteRepositories
{
    public class WriteCatgoryRepository
    {
        private readonly ApplicationDbContext _db;

        public WriteCatgoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<CatgoryEntityModel> GetCatgoryAsync(string name, CancellationToken cancellationToken = default)
        {
            return _db.Catgory.FirstOrDefaultAsync(d => d.Name == name, cancellationToken: cancellationToken);
        }

        public async Task AddCatgoryAsync(CatgoryEntityModel Catgory, CancellationToken cancellationToken = default)
        {
            await _db.Catgory.AddAsync(Catgory, cancellationToken);
        }
    }
}