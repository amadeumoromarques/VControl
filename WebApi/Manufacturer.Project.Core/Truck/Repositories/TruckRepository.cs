namespace Manufacturer.Project.Core.Repositories
{
    using Manufacturer.Project.Core.Base;
    using Manufacturer.Project.Core.DataContext;
    using Manufacturer.Project.Domain.Models;
    using Manufacturer.Project.Domain.Repositories;

    public class TruckRepository : BaseRepository<Truck, long, ProjectDBContext>, ITruckRepository
    {
        /// <summary>
        /// Inicializa uma nova instância do DBContext na aplicação <see cref="TruckRepository"/> class.
        /// </summary>
        /// <param name="objContext">Database context.</param>
        public TruckRepository(ProjectDBContext objContext)
            : base(objContext)
        {            
        }
    }
}
