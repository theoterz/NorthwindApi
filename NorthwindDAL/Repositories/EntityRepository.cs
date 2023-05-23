using Microsoft.EntityFrameworkCore;
using NorthwindDAL.DataContext;
using NorthwindDAL.Interfaces;
using NorthwindModels.Models;
using System.Linq.Expressions;

namespace NorthwindDAL.Repositories
{
    public class EntityRepository<EntityType, IdType> : IEntityRepository<EntityType, IdType> where EntityType : class, IEntity<IdType>
    {
        private readonly AppDbContext _appDbContext;
        public EntityRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateEntityAsync(EntityType entity)
        {
            await _appDbContext.Set<EntityType>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteEntityAsync(EntityType entity)
        {
            _appDbContext.Set<EntityType>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public bool EntityExists(IdType id)
        {
            return _appDbContext.Set<EntityType>().Any(e => e.Id!.Equals(id));
        }

        public async Task<IEnumerable<EntityType>> GetAllEntitiesAsync()
        {
            return await _appDbContext.Set<EntityType>().ToListAsync();
        }

        public async Task<IEnumerable<EntityType>> GetAllEntitiesAsync(Expression<Func<EntityType, bool>> filter)
        {
            return await _appDbContext.Set<EntityType>().Where(filter).ToListAsync();
        }

        public async Task<EntityType?> GetEntityByIdAsync(IdType id)
        {
            return await _appDbContext.Set<EntityType>().FindAsync(id);
        }

        public async Task UpdateEntityAsync(EntityType entity)
        {
            _appDbContext.Set<EntityType>().Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
