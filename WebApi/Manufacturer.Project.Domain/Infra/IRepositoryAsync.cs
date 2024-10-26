namespace Manufacturer.Project.Domain.Infra
{
    using System.Threading.Tasks;

    public interface IRepositoryAsync<TEntity, IDTYPE> where TEntity : class, IBaseEntity
    {
        ValueTask<TEntity> FindByIdAsync(object id);

        Task<int> SaveChangesAsync();
    }
}