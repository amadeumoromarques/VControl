namespace Manufacturer.Project.Core.Base
{
    using Manufacturer.Project.Domain.Infra;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseRepository<TEntity, IDTYPE, TDbContext> : IRepository<TEntity, IDTYPE> where TEntity : class, IBaseEntity where TDbContext : DbContext
    {
        private DbSet<TEntity> _databaseSet;

        public DbContext Context { get; private set; }

        public DbSet<TEntity> DataSet => _databaseSet;

        public BaseRepository(TDbContext context)
        {
            Context = context;
            _databaseSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DataSet;
        }

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return DataSet.AsNoTracking();
        }

        public virtual TEntity FindById(object id)
        {
            return DataSet.Find(id);
        }

        public virtual ValueTask<TEntity> FindByIdAsync(object id)
        {
            return DataSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetById(IDTYPE id)
        {
            return DataSet.Where((r) => r.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> GetByIdNoTracking(IDTYPE id)
        {
            return from r in DataSet.AsNoTracking()
                   where r.Id.Equals(id)
                   select r;
        }

        public virtual void Insert(TEntity obj)
        {
            DataSet.Add(obj);
        }

        public virtual void Update(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Remove(TEntity obj)
        {
            DataSet.Remove(obj);
        }

        public virtual void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Exceções relacionadas ao banco de dados
                Console.WriteLine($"Erro ao salvar as mudanças: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Outras exceções
                Console.WriteLine($"Erro: {ex.Message}");
                throw;
            }
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}