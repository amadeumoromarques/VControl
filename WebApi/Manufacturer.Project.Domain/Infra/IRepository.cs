namespace Manufacturer.Project.Domain.Infra
{
    using Microsoft.EntityFrameworkCore;
    
    public interface IRepository<TEntity, IDTYPE> : IRepositoryAsync<TEntity, IDTYPE> where TEntity : class, IBaseEntity
    {
        DbContext Context { get; }

        DbSet<TEntity> DataSet { get; }

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllNoTracking();

        IQueryable<TEntity> GetById(IDTYPE id);

        IQueryable<TEntity> GetByIdNoTracking(IDTYPE id);

        TEntity FindById(object id);

        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Remove(TEntity obj);

        void SaveChanges();
    }
}