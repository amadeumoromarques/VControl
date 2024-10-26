namespace Manufacturer.Project.Core.Base
{
    using Manufacturer.Project.Domain.Infra;
    using Microsoft.EntityFrameworkCore;

    public class BaseEntityModifiedService<TEntityModified, TType, TRepository> : BaseDataService<TEntityModified, TType, TRepository>
        where TEntityModified : class, IBaseEntity, IEntityModified
        where TRepository : IRepository<TEntityModified, TType>
    {
        protected BaseEntityModifiedService(TRepository objRepository)
            : base(objRepository)
        {
        }

        public override TEntityModified Remove(TType id)
        {
            var dataBaseEntity = objServiceRepository
                .GetById(id)
                .IgnoreQueryFilters()
                .FirstOrDefault();

            if (dataBaseEntity == null)
            {
                return null;
            }

            if (dataBaseEntity.Deleted == null)
            {
                dataBaseEntity.Deleted = DateTime.Now;
            }
            else
            {
                dataBaseEntity.Deleted = null;
            }

            objServiceRepository.SaveChanges();

            return dataBaseEntity;
        }
    }
}