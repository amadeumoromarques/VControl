using Microsoft.AspNet.OData;

namespace Manufacturer.Project.Domain.Infra
{
    public interface IDataServiceAsync<TEntity, IDTYPE> where TEntity : class
    {
        Task<TEntity> SaveAsync(TEntity obj);

        Task<TEntity> SaveAsync(TEntity obj, Func<TEntity, Task> beforeSaveAsync);

        Task<TEntity> CreateAsync(TEntity obj);

        Task<TEntity> UpdateAsync(TEntity obj, UpdateEntityAsync<TEntity> update = null);

        Task<TEntity> PatchAsync(Delta<TEntity> obj);

        Task<TEntity> RemoveAsync(IDTYPE id);
    }
}