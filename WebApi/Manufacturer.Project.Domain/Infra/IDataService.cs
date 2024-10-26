using Microsoft.AspNet.OData;

namespace Manufacturer.Project.Domain.Infra
{
    public interface IDataService<TEntity, IDTYPE> : IDataService<TEntity>, IDataServiceAsync<TEntity, IDTYPE> where TEntity : class
    {
        IQueryable<TEntity> GetById(IDTYPE id);

        IQueryable<TEntity> GetByIdNoTracking(IDTYPE id);

        TEntity Remove(IDTYPE id);
    }

    public interface IDataService<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        
        IQueryable<TEntity> GetAllNoTracking();

        TEntity Save(TEntity obj);

        TEntity Patch(Delta<TEntity> obj);

        TEntity Remove(object id);
    }
}