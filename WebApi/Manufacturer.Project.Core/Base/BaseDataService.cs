namespace Manufacturer.Project.Core.Base
{

    using Manufacturer.Project.Domain.Infra;
    using Microsoft.AspNet.OData;

    public class BaseDataService<TEntity, IDTYPE, TRepository> : IDataService<TEntity, IDTYPE>, IDataService<TEntity>, IDataServiceAsync<TEntity, IDTYPE>
        where TEntity : class, IBaseEntity where TRepository : IRepository<TEntity, IDTYPE>
    {
        protected readonly TRepository objServiceRepository;

        public BaseDataService(TRepository objRepository)
        {
            this.objServiceRepository = objRepository;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return objServiceRepository.GetAll();
        }

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return objServiceRepository.GetAllNoTracking();
        }

        public IQueryable<TEntity> GetById(object id)
        {
            return GetById((IDTYPE)id);
        }

        public virtual IQueryable<TEntity> GetById(IDTYPE id)
        {
            return objServiceRepository.GetById(id);
        }

        public IQueryable<TEntity> GetByIdNoTracking(object id)
        {
            return GetByIdNoTracking((IDTYPE)id);
        }

        public virtual IQueryable<TEntity> GetByIdNoTracking(IDTYPE id)
        {
            return objServiceRepository.GetByIdNoTracking(id);
        }

        public virtual TEntity Save(TEntity obj)
        {
            var returnSave = SaveAsync(obj).Result;
            return returnSave;
        }

        public virtual Task<TEntity> SaveAsync(TEntity obj)
        {
            return SaveAsync(obj, null);
        }

        public virtual async Task<TEntity> SaveAsync(TEntity obj, Func<TEntity, Task> beforeSaveAsync)
        {
            TEntity dataBaseEntity = await objServiceRepository.FindByIdAsync(obj.Id);

            if (!Equals(obj.Id, default(IDTYPE)) && dataBaseEntity == null)
            {
                throw new Exception($"Id: Entidade não encontrada para o Id: {obj.Id}");
            }

            if (dataBaseEntity != null)
            {
                objServiceRepository.Context.Entry(dataBaseEntity).CurrentValues.SetValues(obj);
            }
            else
            {
                objServiceRepository.Insert(obj);
                dataBaseEntity = obj;
            }

            if (beforeSaveAsync != null)
            {
                await beforeSaveAsync(dataBaseEntity);
            }

            await objServiceRepository.SaveChangesAsync();
            return dataBaseEntity;
        }

        public virtual TEntity Create(TEntity obj)
        {
            return CreateAsync(obj).Result;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity obj)
        {
            if (await objServiceRepository.FindByIdAsync((IDTYPE)obj.Id) != null)
            {
                throw new Exception($"EntityAlreadyExists: Entidade já existe, não é possível criar uma nova.");
            }

            objServiceRepository.Insert(obj);
            TEntity dataBaseEntity = obj;
            await objServiceRepository.SaveChangesAsync();
            return dataBaseEntity;
        }

        public virtual TEntity Update(TEntity obj)
        {
            return UpdateAsync(obj).Result;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity obj, UpdateEntityAsync<TEntity> update = null)
        {
            TEntity dataBaseEntity = await objServiceRepository.FindByIdAsync((IDTYPE)obj.Id);
            if (dataBaseEntity != null)
            {
                objServiceRepository.Update(dataBaseEntity);
                objServiceRepository.Context.Entry(dataBaseEntity).CurrentValues.SetValues(obj);
                if (update != null)
                {
                    await update(dataBaseEntity);
                }

                await objServiceRepository.SaveChangesAsync();
                return dataBaseEntity;
            }

            throw new Exception($"Entity não foi encontrado, erro ao efetuar o UpdateAsync");
        }

        public virtual TEntity Patch(Delta<TEntity> obj)
        {
            return PatchAsync(obj).Result;
        }

        public virtual async Task<TEntity> PatchAsync(Delta<TEntity> obj)
        {
            TEntity dataBaseEntity = await objServiceRepository.FindByIdAsync(obj.GetInstance().Id);
            if (dataBaseEntity == null)
            {
                throw new Exception($"Id: Entidade não encontrada para o Id: {obj.GetInstance().Id}");
            }

            obj.Patch(dataBaseEntity);
            await objServiceRepository.SaveChangesAsync();
            return dataBaseEntity;
        }

        public TEntity Remove(object id)
        {
            return Remove((IDTYPE)id);
        }

        public virtual TEntity Remove(IDTYPE id)
        {
            return RemoveAsync(id).Result;
        }

        public virtual async Task<TEntity> RemoveAsync(IDTYPE id)
        {
            TEntity dataBaseEntity = await objServiceRepository.FindByIdAsync(id);
            objServiceRepository.Remove(dataBaseEntity);
            await objServiceRepository.SaveChangesAsync();
            return dataBaseEntity;
        }
    }
}