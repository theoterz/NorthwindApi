using NorthwindModels.Models;
using System.Linq.Expressions;

namespace NorthwindDAL.Interfaces
{
    public interface IEntityRepository<EntityType, IdType> where EntityType : IEntity<IdType>
    {
        public Task<IEnumerable<EntityType>> GetAllEntitiesAsync();
        public Task<IEnumerable<EntityType>> GetAllEntitiesAsync(Expression<Func<EntityType, bool>> filter);
        public Task<EntityType?> GetEntityByIdAsync(IdType id);
        public Task CreateEntityAsync(EntityType entity);
        public Task UpdateEntityAsync(EntityType entity);
        public Task DeleteEntityAsync(EntityType entity);
        public bool EntityExists(IdType id);
    }
}
