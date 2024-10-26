namespace Manufacturer.Project.Core.Repositories
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Core.DataContext;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;

    public class TruckTypeRepository : BaseRepository<TruckType, long, ProjectDBContext>, ITruckTypeRepository
    {
        /// <summary>
        /// Inicializa uma nova instância do DBContext na aplicação <see cref="TruckTypeRepository"/> class.
        /// </summary>
        /// <param name="objContext">Database context.</param>
        public TruckTypeRepository(ProjectDBContext objContext)
            : base(objContext)
        {            
        }
    }
}
